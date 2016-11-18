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

            if (mode == 1)
            {
                Convert.ToByte(stream.ReadByte());
                Convert.ToByte(stream.ReadByte());
                Convert.ToByte(stream.ReadByte());
            }
            else if (mode == 2)
                Convert.ToByte(stream.ReadByte());
            else if (mode == 3)
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

                if (mode == 1)
                {
                    packer = new FullPacker();
                    packer.Compressor = GetCompressor(input.ReadByte());
                    ICipher cipher = GetCipher(input.ReadByte(), key);
                    packer.Encryptor = GetEncryptor(input.ReadByte(), cipher, input);
                }
                else if (mode == 2)
                    packer = new CompressionPacker { Compressor = GetCompressor(input.ReadByte())} ;
                else if (mode == 3)
                {
                    packer = new EncryptionPacker();
                    ICipher cipher = GetCipher(input.ReadByte(), key);
                    packer.Encryptor = GetEncryptor(input.ReadByte(), cipher, input);
                    packer.Encryptor.Cipher = cipher;
                }
                else
                    throw new ArgumentException(); // inny typ wyjątku?
            }

            return packer;
        }

        private ICompressor GetCompressor(int id)
        {
            switch (id)
            {
                case 1:
                    return new LZ77();
                case 2:
                    return new LZ78();
                case 3:
                    return new LZW();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ICipher GetCipher(int id, byte[] key)
        {
            key = KeyExtender.Extend(key, new PKCS7Padding()); // to chyba powinien encryptor robić
            switch (id)
            {
                case 1:
                    return new AES(key);
                case 2:
                    return new Serpent(key);
                case 3:
                    return new Twofish(key);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEncryptor GetEncryptor(int id, ICipher cipher, Stream input)
        {
            switch (id)
            {
                case 1:
                    return new ECB(cipher, new PKCS7Padding());
                case 2:
                    {
                        var iv = new byte[cipher.BlockSize];

                        for (int i = 0; i < iv.Length; i++)
                            iv[i] = Convert.ToByte(input.ReadByte());

                        return new CBC(cipher, new PKCS7Padding(), cipher.Decrypt(iv));
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public byte[] GetBytes(ICompressor compressor, IEncryptor encryptor)
        {
            var header = new List<byte> { GetModeId(compressor, encryptor) };

            if (compressor != null) // nie potrzebne
            {
                if (compressor is LZ77)
                    header.Add(1);
                else if (compressor is LZ78)
                    header.Add(2);
                else if(compressor is LZW)
                    header.Add(3);
            }

            if (encryptor?.Cipher != null)
            {
                if (encryptor.Cipher is AES)
                    header.Add(1);
                else if(encryptor.Cipher is Serpent)
                    header.Add(2);
                else if(encryptor.Cipher is Twofish)
                    header.Add(3);

                if (encryptor is ECB)
                    header.Add(1);
                if (encryptor is CBC)
                    header.Add(2);
            }

            return header.ToArray();
        }

        private byte GetModeId(ICompressor compressor, IEncryptor encryptor)
        {
            if (compressor != null && encryptor != null)
                return 1;

            if (compressor != null)
                return 2;

            if (encryptor != null)
                return 3;

            throw new ArgumentOutOfRangeException();
        }
    }
}