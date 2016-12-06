using CryptZip.Compression;
using CryptZip.Encryption;
using System.Threading.Tasks;

namespace CryptZip
{
    public interface IPacker
    {
        ICompressor Compressor { get; set; }
        IEncryptor Encryptor { get; set; }
        event Packer.StatusChangedHandler StatusChanged;
        event Packer.WorkFinishedHandler WorkFinished;
        Task PackAsync(string path);
        Task UnpackAsync(string path);
    }
}
