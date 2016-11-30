using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests
{
    [TestClass]
    public class temp
    {
        //[TestMethod]
        //public void aes()
        //{
        //    var key = new byte[] {0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x7, 0x8, 0x9, 0x00, 0x01, 0x02, 0x03, 0x04, 0x5, 0x6, 0x7, 0x8, 0x9, 0x00, 0x01, 0x02 };
        //    var aes = new Serpent(key);
        //    var ecb = new CBC(aes, new PKCS7Padding(), new byte[] { 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 });
        //    var stream = new FileStream(@"D:\test2.txt", FileMode.Open, FileAccess.Read);
        //    var output = new FileStream(@"D:\cryptzip.txt", FileMode.Create, FileAccess.Write);
        //    ecb.Encrypt(stream, output);
        //    stream.Close();
        //    output.Close();

        //    stream = new FileStream(@"D:\cryptzip.txt", FileMode.Open, FileAccess.Read);
        //    output = new FileStream(@"D:\cryptzip - decrypted.txt", FileMode.Create, FileAccess.Write);
        //    ecb.Decrypt(stream, output);
        //}

        //[TestMethod]
        //public void compress()
        //{
        //    var alg = new LZ77(26, 10);
        //    string text = "alf_eastman_easily_yells_AAAAAAAAAAAAAAAAH"; // 26, 10
        //    var input = new MemoryStream(Encoding.ASCII.GetBytes(text));
        //    //var output = new FileStream(@"D:\cryptzip.txt", FileMode.Create, FileAccess.Write);
        //    alg.Compress(input, new MemoryStream());
        //    input.Close();
        //}
    }
}
