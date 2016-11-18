using System.Threading.Tasks;

namespace CryptZip
{
    public class CompressionPacker : Packer
    {
        public override async Task PackAsync(string path)
        {
            await base.PackAsync(path);

            OnStatusChanged("Compressing...");
            await Task.Run(() => Compressor.Compress(Input, Output));
            Finish();
        }

        public override async Task UnpackAsync(string path)
        {
            await base.UnpackAsync(path);

            OnStatusChanged("Decompressing...");
            await Task.Run(() => Compressor.Decompress(Input, Output));
            Finish();
        }
    }
}