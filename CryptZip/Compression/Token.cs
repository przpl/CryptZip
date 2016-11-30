namespace CryptZip.Compression
{
    public class Token
    {
        public bool Empty { get; set; }

        public int Offset { get; set; }

        public int Length { get; set; }

        public byte Byte { get; set; }
    }
}