using System.IO;
using System.Threading.Tasks;

namespace CryptZip
{
    public class FullPacker : Packer
    {
        public override async Task PackAsync(string path)
        {
            await base.PackAsync(path);

            OnStatusChanged("Compressing...");
            var intermediate = new MemoryStream(); // call compressor every block size bytes
            await Task.Run(() => Compressor.Compress(Input, intermediate));

            OnStatusChanged("Encrypting...");
            intermediate.Position = 0;
            await Task.Run(() => Encryptor.Encrypt(intermediate, Output));

            Finish();
        }

        public override async Task UnpackAsync(string path)
        {
            await base.UnpackAsync(path);

            OnStatusChanged("Decompressing...");
            var intermediate = new MemoryStream();
            await Task.Run(() => Encryptor.Decrypt(Input, intermediate));

            OnStatusChanged("Decrypting...");
            intermediate.Position = 0;
            await Task.Run(() => Compressor.Decompress(intermediate, Output));

            Finish();
        }
    }
}
