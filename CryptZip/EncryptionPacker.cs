using System.Threading.Tasks;

namespace CryptZip
{
    public class EncryptionPacker : Packer
    {
        public override async Task PackAsync(string path)
        {
            await base.PackAsync(path);

            OnStatusChanged("Encrypting...");
            await Task.Run(() => Encryptor.Encrypt(Input, Output));
            Finish();
        }

        public override async Task UnpackAsync(string path)
        {
            await base.UnpackAsync(path);

            OnStatusChanged("Decrypting...");
            await Task.Run(() => Encryptor.Decrypt(Input, Output));
            Finish();
        }
    }
}