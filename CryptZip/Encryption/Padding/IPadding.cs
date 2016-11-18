
namespace CryptZip.Encryption.Padding
{
    public interface IPadding
    {
        void Add(byte[] bytes);
        void Add(byte[] bytes, int lastByteIndex);
        byte[] Remove(byte[] bytes);
    }
}