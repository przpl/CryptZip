namespace CryptZip.Compression
{
    public class Token
    {
        public int Offset { get; set; }

        public int Length { get; set; }

        public byte Byte { get; set; }

        public static Token Empty(byte b)
        {
            return new Token { Byte = b };
        }
    }
}