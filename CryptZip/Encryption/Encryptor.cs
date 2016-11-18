using CryptZip.Encryption.Padding;
using System;
using System.IO;

namespace CryptZip.Encryption
{
    public abstract class Encryptor : IEncryptor
    {
        public ICipher Cipher { get; set; }

        protected readonly IPadding Padding;
        protected byte[] Block;
        protected long Index;
        protected Stream Input, Output;

        protected Encryptor(ICipher cipher, IPadding padding)
        {
            Cipher = cipher;
            Padding = padding;
        }

        public virtual void Encrypt(Stream input, Stream output)
        {
            Input = input;
            Output = output;
            ValidateArguments();

            Index = input.Position;
            Block = new byte[Cipher.BlockSize];
        }

        public virtual void Decrypt(Stream input, Stream output)
        {
            Input = input;
            Output = output;
            ValidateArguments();

            Index = input.Position;
            Block = new byte[Cipher.BlockSize];


            if ((input.Length - input.Position)%Cipher.BlockSize != 0)
                throw new ArgumentException(nameof(input), "Input data must be divisible by " + Cipher.BlockSize + ".");
        }

        protected void ReadBlock()
        {
            var blockIndex = 0;
            while (blockIndex < Block.Length)
            {
                Block[blockIndex++] = (byte) Input.ReadByte();
                Index++;

                if (Index == Input.Length && blockIndex < Block.Length)
                {
                    Padding.Add(Block, blockIndex - 1);
                    break;
                }
            }
        }

        protected void WriteBlock()
        {
            Output.Write(Block, 0, Block.Length);
            Output.Flush();
        }

        protected bool IsLastBlock()
        {
            return Index > Input.Length - 16;
        }

        private void ValidateArguments()
        {
            if (Input == null)
                throw new ArgumentNullException(nameof(Input), "Input stream is null.");
            if (Input.Length == 0)
                throw new ArgumentException(nameof(Input), "Input stream is empty.");
            if (!Input.CanRead)
                throw new ArgumentException(nameof(Input), "Cannot read from input stream.");
            if (Output == null)
                throw new ArgumentNullException(nameof(Output), "Output stream is null.");
            if (!Output.CanWrite)
                throw new ArgumentException(nameof(Output), "Cannot write to output stream.");
        }
    }
}