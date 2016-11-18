
namespace CryptZip.Encryption
{
    public interface ICipher
    {
        int BlockSize { get; }
        byte[] Encrypt(byte[] block);
        byte[] Decrypt(byte[] block);
    }
}