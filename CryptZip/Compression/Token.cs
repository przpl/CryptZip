namespace CryptZip.Compression
{
    public class Token
    {
        public int Offset { get; set; }

        public int Length { get; set; }

        public int Byte { get; set; }

        public static Token Empty(int b)
        {
            return new Token { Byte = b };
        }
    }
}