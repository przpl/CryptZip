namespace CryptZip
{
    public static class Mode
    {
        public static byte Full = 1;
        public static byte Compress = 2;
        public static byte Encrypt = 3;
    }

    public static class CompressorId
    {
        public static byte LZ77 = 1;
        public static byte LZ78 = 2;
        public static byte LZW = 3;
    }

    public static class CipherId
    {
        public static byte AES = 1;
        public static byte Serpent = 2;
        public static byte Twofish = 3;
    }

    public static class EncryptorId
    {
        public static byte ECB = 1;
        public static byte CBC = 2;
    }
}