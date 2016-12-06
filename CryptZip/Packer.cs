using CryptZip.Compression;
using CryptZip.Encryption;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CryptZip
{
    public abstract class Packer : IPacker
    {
        public ICompressor Compressor { get; set; }
        public IEncryptor Encryptor { get; set; } // interfejs zamiast klasy

        public delegate void StatusChangedHandler(object source, StatusEventArgs args);
        public event StatusChangedHandler StatusChanged;
        public delegate void WorkFinishedHandler(object source, EventArgs args);
        public event WorkFinishedHandler WorkFinished;

        protected Stream Input, Output;

        private const string FILE_EXTENSTION = "czp";

        public static bool IsPackedFile(string path)
        {
            // sprawdzanie nagłówków

            return path.EndsWith(FILE_EXTENSTION);
        }

        public static bool IsEncrypted(string path) // testy
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                if (stream.Length == 0)
                    return false;

                int modeId = stream.ReadByte();

                if (modeId == Mode.Full || modeId == Mode.Encrypt)
                    return true;

                return false;
            }
        }

        public virtual async Task PackAsync(string path)
        {
            Input = new FileStream(path, FileMode.Open, FileAccess.Read);
            Output = new FileStream(path + FILE_EXTENSTION, FileMode.Create, FileAccess.Write);

            WriteHeader(Output);
        }

        protected void WriteHeader(Stream output)
        {
            var fileHeader = new FileHeader().GetBytes(Compressor, Encryptor);

            output.Write(fileHeader, 0, fileHeader.Length);

            if (Encryptor is CBC) // chyba nie powinna mieć wiedzy na temat trybów, CBC
            {
                var cbc = Encryptor as CBC;
                var iv = (byte[])cbc.IV.Clone();
                var encrypted = Encryptor.Cipher.Encrypt(cbc.IV);
                cbc.IV = iv;
                output.Write(encrypted, 0, encrypted.Length);
            }
        }

        public virtual async Task UnpackAsync(string path)
        {
            Input = new FileStream(path, FileMode.Open, FileAccess.Read);
            Output = new FileStream(path.Replace(FILE_EXTENSTION, ""), FileMode.Create, FileAccess.Write);

            ReadHeader(); // obecnie tylko po to żeby przetwarzać od właściwego miejsca (bez header)
        }

        protected void Finish()
        {
            Close();
            OnStatusChanged("Finished");
            OnWorkFinished();
        }

        private void Close()
        {
            if (Input != null)
            {
                Input.Close();
                Input.Dispose();
                Input = null;
            }

            if (Output != null)
            {
                Output.Close();
                Output.Dispose();
                Output = null;
            }
        }

        protected FileHeader ReadHeader()
        {
            var result = new FileHeader(Input);
            if (Encryptor is CBC)
            {
                var iv = new byte[Encryptor.Cipher.BlockSize];
                Input.Read(iv, 0, iv.Length);
                (Encryptor as CBC).IV = Encryptor.Cipher.Decrypt(iv);
            }
            return result;
        }

        protected virtual void OnStatusChanged(string status)
        {
            StatusChanged?.Invoke(this, new StatusEventArgs { Status = status });
        }

        protected virtual void OnWorkFinished()
        {
            WorkFinished?.Invoke(this, EventArgs.Empty);
        }
    }

    public class StatusEventArgs : EventArgs
    {
        public string Status { get; set; }
    }
}
