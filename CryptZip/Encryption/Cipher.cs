using System;

namespace CryptZip.Encryption
{
    public abstract class Cipher : ICipher
    {
        public int BlockSize { get; } = 16;

        protected int BITS_PER_WORD = 32;

        protected Cipher(byte[] key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key is null.");
            if (key.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(key), "key length has to be greater than zero.");
        }

        public virtual byte[] Encrypt(byte[] block)
        {
            ValidateBlockParameter(block);
            return null;
        }

        public virtual byte[] Decrypt(byte[] block)
        {
            ValidateBlockParameter(block);
            return null;
        }

        private void ValidateBlockParameter(byte[] block)
        {
            if (block == null)
                throw new ArgumentNullException(nameof(block), "Block is null.");
            if (block.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(block), "Block length has to be greater than zero.");
            if (block.Length != BlockSize)
                throw new ArgumentException(nameof(block), "Invalid block size. Should be " + BlockSize + " bytes.");
        }
    }
}