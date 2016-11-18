using System.IO;

namespace CryptZip.Encryption
{
    public interface IEncryptor
    {
        ICipher Cipher { get; set; }
        void Encrypt(Stream input, Stream output);
        void Decrypt(Stream input, Stream output);
    }
}