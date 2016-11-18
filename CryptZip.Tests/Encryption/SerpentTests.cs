using CryptZip.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Encryption
{
    [TestClass]
    public class SerpentTests
    {
        private Serpent serpent128BitKey, serpent192BitKey, serpent256BitKey;

        [TestInitialize]
        public void Init()
        {
            serpent128BitKey = new Serpent(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            serpent192BitKey = new Serpent(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 });
            serpent256BitKey = new Serpent(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 });
        }

        [TestMethod]
        public void Encrypt_EncryptsBlockWith128BitKey_Encrypted()
        {
            byte[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] expected = { 0x81, 0xf3, 0x27, 0x4c, 0x46, 0xd0, 0x13, 0x1a, 0x9e, 0xdb, 0xe7, 0xfc, 0x43, 0xdf, 0x10, 0x98 };
            byte[] result = serpent128BitKey.Encrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decrypt_DecryptsBlockWith128BitKey_Decrypted()
        {
            byte[] data = { 0x81, 0xf3, 0x27, 0x4c, 0x46, 0xd0, 0x13, 0x1a, 0x9e, 0xdb, 0xe7, 0xfc, 0x43, 0xdf, 0x10, 0x98 };
            byte[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] result = serpent128BitKey.Decrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Encrypt_EncryptsBlockWith192BitKey_Encrypted()
        {
            byte[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] expected = { 0xc6, 0x96, 0xbd, 0x9e, 0xbf, 0x58, 0x66, 0x10, 0x5b, 0xae, 0x8c, 0x66, 0x06, 0xfa, 0x2a, 0x26 };
            byte[] result = serpent192BitKey.Encrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decrypt_DecryptsBlockWith192BitKey_Decrypted()
        {
            byte[] data = { 0xc6, 0x96, 0xbd, 0x9e, 0xbf, 0x58, 0x66, 0x10, 0x5b, 0xae, 0x8c, 0x66, 0x06, 0xfa, 0x2a, 0x26 };
            byte[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] result = serpent192BitKey.Decrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Encrypt_EncryptsBlockWith256BitKey_Encrypted()
        {
            byte[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] expected = { 0x90, 0xa6, 0x4a, 0x1b, 0xb7, 0x97, 0x47, 0xee, 0x61, 0x34, 0xdf, 0x92, 0x11, 0xa8, 0x99, 0xea };
            byte[] result = serpent256BitKey.Encrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decrypt_DecryptsBlockWith256BitKey_Decrypted()
        {
            byte[] data = { 0x90, 0xa6, 0x4a, 0x1b, 0xb7, 0x97, 0x47, 0xee, 0x61, 0x34, 0xdf, 0x92, 0x11, 0xa8, 0x99, 0xea };
            byte[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] result = serpent256BitKey.Decrypt(data);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
