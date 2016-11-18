using CryptZip.Encryption.Padding;
using System;
using System.IO;

namespace CryptZip.Encryption
{
    public class CBC : Encryptor
    {
        public byte[] IV { get; set; }

        public CBC(ICipher cipher, IPadding padding, byte[] IV) : base(cipher, padding)
        {
            if (IV == null)
                throw new ArgumentNullException(nameof(IV), "Initialization vector IV is null.");
            if (IV.Length == 0)
                throw new ArgumentNullException(nameof(IV), "Initialization vector IV is empty.");
            if (IV.Length != cipher.BlockSize)
                throw new ArgumentNullException(nameof(IV), "Initialization vector IV length has to be equal to block size.");

            this.IV = IV;
        }

        public override void Encrypt(Stream input, Stream output)
        {
            base.Encrypt(input, output);

            var previousBlock = new byte[IV.Length];
            IV.CopyTo(previousBlock, 0);

            while (Index < input.Length)
            {
                ReadBlock();
                Block = Block.XOR(previousBlock);
                Block = Cipher.Encrypt(Block);
                Block.CopyTo(previousBlock, 0);
                WriteBlock();
            }

            AddPaddingBlock(previousBlock);
        }

        private void AddPaddingBlock(byte[] previousBlock)
        {
            if (Input.Length % Cipher.BlockSize == 0)
            {
                Padding.Add(Block);
                Block = Block.XOR(previousBlock);
                Block = Cipher.Encrypt(Block);
                WriteBlock();
            }
        }

        public override void Decrypt(Stream input, Stream output)
        {
            base.Decrypt(input, output);

            var previousBlock = new byte[IV.Length];
            IV.CopyTo(previousBlock, 0);
            var nextBlock = new byte[IV.Length];
            IV.CopyTo(nextBlock, 0);

            while (Index < input.Length)
            {
                ReadBlock();
                Block.CopyTo(nextBlock, 0);
                Block = Cipher.Decrypt(Block);
                Block = Block.XOR(previousBlock);
                if (IsLastBlock())
                    Block = Padding.Remove(Block);
                WriteBlock();
                nextBlock.CopyTo(previousBlock, 0);
            }
        }
    }
}