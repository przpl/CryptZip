using CryptZip.Encryption;
using CryptZip.Encryption.Padding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.IO;
using System.Linq;

namespace CryptZip.Tests.Encryption
{
    [TestClass]
    public class ECBTests
    {
        [TestMethod]
        public void Encrypt_EncryptsBlock_Encrypted()
        {
            byte[] bytes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] padding = {16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16};
            var blockEncryptorMock = MockRepository.GenerateMock<ICipher>();
            blockEncryptorMock.Expect(m => m.BlockSize).Return(16);
            blockEncryptorMock.Expect(m => m.Encrypt(bytes)).Return(bytes.Reverse().ToArray());
            blockEncryptorMock.Expect(m => m.Encrypt(padding)).Return(padding);

            ECB encryptor = new ECB(blockEncryptorMock, new PKCS7Padding());
            MemoryStream input = new MemoryStream(bytes);
            MemoryStream output = new MemoryStream();
            encryptor.Encrypt(input, output);
            CollectionAssert.AreEqual(new byte[] { 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 },
                output.GetBuffer().SubArray(0, 32));
        }

        [TestMethod]
        public void Decrypt_DecryptsBlock_Decrypted()
        {
            byte[] bytes = { 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
            byte[] firstBlock = { 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            byte[] padding = { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
            var blockEncryptorMock = MockRepository.GenerateMock<ICipher>();
            blockEncryptorMock.Expect(m => m.BlockSize).Return(16);
            blockEncryptorMock.Expect(m => m.Decrypt(firstBlock)).Return(firstBlock.Reverse().ToArray());
            blockEncryptorMock.Expect(m => m.Decrypt(padding)).Return(padding);

            ECB encryptor = new ECB(blockEncryptorMock, new PKCS7Padding());
            MemoryStream input = new MemoryStream(bytes);
            MemoryStream output = new MemoryStream();
            encryptor.Decrypt(input, output);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, output.GetBuffer().SubArray(0, 16));
        }

        [TestMethod]
        public void Encrypt_Encrypts10Bytes_Encrypted()
        {
            byte[] bytes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            byte[] padding = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 6, 6, 6, 6, 6, 6 };
            var blockEncryptorMock = MockRepository.GenerateMock<ICipher>();
            blockEncryptorMock.Expect(m => m.BlockSize).Return(16);
            blockEncryptorMock.Expect(m => m.Encrypt(bytes)).Return(bytes.Reverse().ToArray());
            blockEncryptorMock.Expect(m => m.Encrypt(padding)).Return(padding.Reverse().ToArray());

            ECB encryptor = new ECB(blockEncryptorMock, new PKCS7Padding());
            MemoryStream input = new MemoryStream(bytes);
            MemoryStream output = new MemoryStream();
            encryptor.Encrypt(input, output);
            CollectionAssert.AreEqual(new byte[] { 6, 6, 6, 6, 6, 6, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 },
                output.GetBuffer().SubArray(0, 16));
        }

        [TestMethod]
        public void Decrypt_Decrypts10Bytes_Decrypted()
        {
            byte[] bytes = { 6, 6, 6, 6, 6, 6, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            var blockEncryptorMock = MockRepository.GenerateMock<ICipher>();
            blockEncryptorMock.Expect(m => m.BlockSize).Return(16);
            blockEncryptorMock.Expect(m => m.Decrypt(bytes)).Return(bytes.Reverse().ToArray());
            //blockEncryptorMock.Expect(m => m.DecryptBlock(padding)).Return(padding);

            ECB encryptor = new ECB(blockEncryptorMock, new PKCS7Padding());
            MemoryStream input = new MemoryStream(bytes);
            MemoryStream output = new MemoryStream();
            encryptor.Decrypt(input, output);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, output.GetBuffer().SubArray(0, 10));
        }
    }
}
