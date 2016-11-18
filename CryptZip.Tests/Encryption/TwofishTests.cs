using CryptZip.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Encryption
{
    [TestClass]
    public class TwofishTests
    {
        private Twofish twofish128BitKey, twofish192BitKey, twofish256BitKey;

        [TestInitialize]
        public void Init()
        {
            twofish128BitKey = new Twofish(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            twofish192BitKey = new Twofish(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 });
            twofish256BitKey = new Twofish(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 });
        }

        [TestMethod]
        public void Encrypt_EncryptsBlockWith128BitKey_Encrypted()
        {
            byte[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] expected = { 0x29, 0x6c, 0xae, 0xaa, 0x15, 0x4b, 0xd8, 0x8e, 0x20, 0xad, 0xfa, 0x6a, 0xdc, 0xd6, 0xf4, 0x0c };
            byte[] result = twofish128BitKey.Encrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decrypt_DecryptsBlockWith128BitKey_Decrypted()
        {
            byte[] data = { 0x29, 0x6c, 0xae, 0xaa, 0x15, 0x4b, 0xd8, 0x8e, 0x20, 0xad, 0xfa, 0x6a, 0xdc, 0xd6, 0xf4, 0x0c };
            byte[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] result = twofish128BitKey.Decrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Encrypt_EncryptsBlockWith192BitKey_Encrypted()
        {
            byte[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] expected = { 0xc3, 0x5e, 0x1c, 0x9c, 0x7f, 0x0d, 0xcc, 0xc1, 0x69, 0xf9, 0x0b, 0xbe, 0x79, 0xcf, 0xd6, 0xe1 };
            byte[] result = twofish192BitKey.Encrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decrypt_DecryptsBlockWith192BitKey_Decrypted()
        {
            byte[] data = { 0xc3, 0x5e, 0x1c, 0x9c, 0x7f, 0x0d, 0xcc, 0xc1, 0x69, 0xf9, 0x0b, 0xbe, 0x79, 0xcf, 0xd6, 0xe1 };
            byte[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] result = twofish192BitKey.Decrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Encrypt_EncryptsBlockWith256BitKey_Encrypted()
        {
            byte[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] expected = { 0xa1, 0xb1, 0xd0, 0xda, 0x9d, 0x81, 0x2a, 0x53, 0x3c, 0x92, 0x18, 0xc7, 0x83, 0xbd, 0x44, 0x1f };
            byte[] result = twofish256BitKey.Encrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decrypt_DecryptsBlockWith256BitKey_Decrypted()
        {
            byte[] data = { 0xa1, 0xb1, 0xd0, 0xda, 0x9d, 0x81, 0x2a, 0x53, 0x3c, 0x92, 0x18, 0xc7, 0x83, 0xbd, 0x44, 0x1f };
            byte[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] result = twofish256BitKey.Decrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
