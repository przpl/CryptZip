using CryptZip.Encryption.Padding;
using System.IO;

namespace CryptZip.Encryption
{
    public class ECB : Encryptor
    {
        public ECB(ICipher cipher, IPadding padding) : base(cipher, padding)
        { }

        public override void Encrypt(Stream input, Stream output)
        {
            base.Encrypt(input, output);

            while (DataNotEnded())
            {
                ReadBlock();
                Block = Cipher.Encrypt(Block);
                WriteBlock();
            }

            AddPaddingBlock();
        }

        private void AddPaddingBlock()
        {
            if (Input.Length % Cipher.BlockSize == 0)
            {
                Padding.Add(Block);
                Block = Cipher.Encrypt(Block);
                WriteBlock();
            }
        }

        public override void Decrypt(Stream input, Stream output)
        {
            base.Decrypt(input, output);

            while (DataNotEnded())
            {
                ReadBlock();
                Block = Cipher.Decrypt(Block);
                if (IsLastBlock())
                    Block = Padding.Remove(Block);
                WriteBlock();
            }
        }
    }
}