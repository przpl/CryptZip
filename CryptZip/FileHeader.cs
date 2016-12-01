using CryptZip.Compression;
using CryptZip.Encryption;
using CryptZip.Encryption.Padding;
using System;
using System.Collections.Generic;
using System.IO;

namespace CryptZip
{
    public class FileHeader
    {
        public FileHeader()
        { }

        // nie potrzebne, służy tylko do zmiany pozycji wskaźnika w strumieniu
        public FileHeader(Stream stream)
        {
            byte mode = Convert.ToByte(stream.ReadByte());

            if (mode == Mode.Full)
            {
                Convert.ToByte(stream.ReadByte());
                Convert.ToByte(stream.ReadByte());
                Convert.ToByte(stream.ReadByte());
            }
            else if (mode == Mode.Compress)
                Convert.ToByte(stream.ReadByte());
            else if (mode == Mode.Encrypt)
            {
                Convert.ToByte(stream.ReadByte());
                Convert.ToByte(stream.ReadByte());
            }
        }

        public Packer GetPacker(string path, byte[] key = null)
        {
            Packer packer = null;

            using (var input = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte mode = Convert.ToByte(input.ReadByte());

                if (mode == Mode.Full)
                {
                    packer = new FullPacker();
                    packer.Compressor = GetCompressor(input.ReadByte());
                    ICipher cipher = GetCipher(input.ReadByte(), key);
                    packer.Encryptor = GetEncryptor(input.ReadByte(), cipher, input);
                }
                else if (mode == Mode.Compress)
                    packer = new CompressionPacker { Compressor = GetCompressor(input.ReadByte())} ;
                else if (mode == Mode.Encrypt)
                {
                    packer = new EncryptionPacker();
                    ICipher cipher = GetCipher(input.ReadByte(), key);
                    packer.Encryptor = GetEncryptor(input.ReadByte(), cipher, input);
                    packer.Encryptor.Cipher = cipher;
                }
                else
                    throw new InvalidOperationException("Invalid file header");
            }

            return packer;
        }

        private ICompressor GetCompressor(int id)
        {
            if (id == CompressorId.LZ77)
                return new LZ77();

            if (id == CompressorId.LZ78)
                return new LZ78();

            if (id == CompressorId.LZW)
                return new LZW();

            throw new ArgumentOutOfRangeException();
        }

        private ICipher GetCipher(int id, byte[] key)
        {
            key = KeyExtender.Extend(key, new PKCS7Padding()); // to chyba powinien encryptor robić

            if (id == CipherId.AES)
                return new AES(key);

            if (id == CipherId.Serpent)
                return new Serpent(key);

            if (id == CipherId.Twofish)
                return new Twofish(key);

            throw new ArgumentOutOfRangeException();
        }

        private IEncryptor GetEncryptor(int id, ICipher cipher, Stream input)
        {
            if (id == EncryptorId.ECB)
                return new ECB(cipher, new PKCS7Padding());

            if (id == EncryptorId.CBC)
            {
                var iv = new byte[cipher.BlockSize];

                for (int i = 0; i < iv.Length; i++)
                    iv[i] = Convert.ToByte(input.ReadByte());

                return new CBC(cipher, new PKCS7Padding(), cipher.Decrypt(iv));
            }

            throw new ArgumentOutOfRangeException();
        }

        public byte[] GetBytes(ICompressor compressor, IEncryptor encryptor)
        {
            var header = new List<byte> { GetModeId(compressor, encryptor) };

            if (compressor != null) // nie potrzebne
            {
                if (compressor is LZ77)
                    header.Add(CompressorId.LZ77);
                else if (compressor is LZ78)
                    header.Add(CompressorId.LZ78);
                else if(compressor is LZW)
                    header.Add(CompressorId.LZW);
            }

            if (encryptor?.Cipher != null)
            {
                if (encryptor.Cipher is AES)
                    header.Add(CipherId.AES);
                else if(encryptor.Cipher is Serpent)
                    header.Add(CipherId.Serpent);
                else if(encryptor.Cipher is Twofish)
                    header.Add(CipherId.Twofish);

                if (encryptor is ECB)
                    header.Add(EncryptorId.ECB);
                if (encryptor is CBC)
                    header.Add(EncryptorId.CBC);
            }

            return header.ToArray();
        }

        private byte GetModeId(ICompressor compressor, IEncryptor encryptor)
        {
            if (compressor != null && encryptor != null)
                return Mode.Full;

            if (compressor != null)
                return Mode.Compress;

            if (encryptor != null)
                return Mode.Encrypt;

            throw new ArgumentOutOfRangeException();
        }
    }
}