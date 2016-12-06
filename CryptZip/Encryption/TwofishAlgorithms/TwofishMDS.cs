namespace CryptZip.Encryption.TwofishAlgorithms
{
    public interface ITwofishMDS
    {
        uint Multiply(uint[] y);
    }

    public class TwofishMDS : ITwofishMDS
    {
        private readonly uint[][] _MDS =
        {
            new uint[256],
            new uint[256],
            new uint[256],
            new uint[256]
        };

        public TwofishMDS()
        {
            for (int i = 0; i < 256; i++)
            {
                _MDS[0][i] = (uint)(TwofishFunction.q1(i) | Mx_X(TwofishFunction.q1(i)) << 8 | Mx_Y(TwofishFunction.q1(i)) << 16 | Mx_Y(TwofishFunction.q1(i)) << 24);
                _MDS[1][i] = (uint)(Mx_Y(TwofishFunction.q0(i)) | Mx_Y(TwofishFunction.q0(i)) << 8 | Mx_X(TwofishFunction.q0(i)) << 16 | TwofishFunction.q0(i) << 24);
                _MDS[2][i] = (uint)(Mx_X(TwofishFunction.q1(i)) | Mx_Y(TwofishFunction.q1(i)) << 8 | TwofishFunction.q1(i) << 16 | Mx_Y(TwofishFunction.q1(i)) << 24);
                _MDS[3][i] = (uint)(Mx_X(TwofishFunction.q0(i)) | TwofishFunction. q0(i) << 8 | Mx_Y(TwofishFunction.q0(i)) << 16 | Mx_X(TwofishFunction.q0(i)) << 24);
            }
        }

        public uint Multiply(uint[] y)
        {
            for (int i = 0; i < 4; i++)
                y[i] = _MDS[i][y[i]];

            return y[0] ^ y[1] ^ y[2] ^ y[3];
        }

        private int Mx_X(int x)
        {
            return x ^ LFSR2(x);
        }

        private int Mx_Y(int x)
        {
            return x ^ LFSR1(x) ^ LFSR2(x);
        }

        private int LFSR1(int x)
        {
            return (x >> 1) ^ ((x & 0x01) != 0 ? 0x169 / 2 : 0);
        }

        private int LFSR2(int x)
        {
            return (x >> 2) ^ ((x & 0x02) != 0 ? 0x169 / 2 : 0) ^ ((x & 0x01) != 0 ? 0x169 / 4 : 0);
        }
    }
}
