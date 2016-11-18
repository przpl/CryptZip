using CryptZip.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Encryption
{
    [TestClass]
    public class AESTests
    {
        private AES aes128BitKey, aes192BitKey, aes256BitKey;
        
        [TestInitialize]
        public void Init()
        {
            aes128BitKey = new AES(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            aes192BitKey = new AES(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 });
            aes256BitKey = new AES(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 });
        }

        [TestMethod]
        public void Encrypt_EncryptsBlockWith128BitKey_Encrypted()
        {
            byte[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] expected = { 0x34, 0xc3, 0x3b, 0x7f, 0x14, 0xfd, 0x53, 0xdc, 0xea, 0x25, 0xe0, 0x1a, 0x02, 0xe1, 0x67, 0x27 };
            byte[] result = aes128BitKey.Encrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decrypt_Decrypts16BytesWith128BitKey_Decrypted()
        {
            byte[] data = { 0x34, 0xc3, 0x3b, 0x7f, 0x14, 0xfd, 0x53, 0xdc, 0xea, 0x25, 0xe0, 0x1a, 0x02, 0xe1, 0x67, 0x27 };
            byte[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] result = aes128BitKey.Decrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Encrypt_EncryptsBlockWith192BitKey_Encrypted()
        {
            byte[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] expected = { 0x41, 0xd1, 0xa5, 0x8d, 0x0c, 0x81, 0xa7, 0xe9, 0xeb, 0xa4, 0x54, 0xea, 0xf1, 0x99, 0xa8, 0xed };
            byte[] result = aes192BitKey.Encrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decrypt_Decrypts16BytesWith192BitKey_Decrypted()
        {
            byte[] data = { 0x41, 0xd1, 0xa5, 0x8d, 0x0c, 0x81, 0xa7, 0xe9, 0xeb, 0xa4, 0x54, 0xea, 0xf1, 0x99, 0xa8, 0xed };
            byte[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] result = aes192BitKey.Decrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Encrypt_EncryptsBlockWith256BitKey_Encrypted()
        {
            byte[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] expected = { 0x4f, 0x01, 0x50, 0x27, 0x37, 0x33, 0x72, 0x7f, 0x99, 0x47, 0x54, 0xfe, 0xe0, 0x54, 0xdf, 0x7e };
            byte[] result = aes256BitKey.Encrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decrypt_Decrypts16BytesWith256BitKey_Decrypted()
        {
            byte[] data = { 0x4f, 0x01, 0x50, 0x27, 0x37, 0x33, 0x72, 0x7f, 0x99, 0x47, 0x54, 0xfe, 0xe0, 0x54, 0xdf, 0x7e };
            byte[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] result = aes256BitKey.Decrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}