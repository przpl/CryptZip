using CryptZip.Encryption;
using CryptZip.Encryption.Padding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CryptZip.Tests
{
    [TestClass]
    public class temp
    {
        [TestMethod]
        public void aes()
        {
            var key = new byte[] {0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 };
            var aes = new Twofish(key);
            var ecb = new ECB(aes, new PKCS7Padding());
            var stream = new FileStream(@"E:\test2.txt", FileMode.Open, FileAccess.Read);
            var output = new FileStream(@"E:\cryptzip.txt", FileMode.Create, FileAccess.Write);
            ecb.Encrypt(stream, output);
            stream.Close();
            output.Close();

            stream = new FileStream(@"E:\cryptzip.txt", FileMode.Open, FileAccess.Read);
            output = new FileStream(@"E:\cryptzip - decrypted.txt", FileMode.Create, FileAccess.Write);
            ecb.Decrypt(stream, output);
        }
    }
}
