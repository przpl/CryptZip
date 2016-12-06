using CryptZip.Encryption.Rijndael;

namespace CryptZip.Encryption
{
    public class AES : Cipher
    {
        private readonly IAesKey _key;
        private byte[] _block;
        private byte[][] _stateMatrix;

        public AES(byte[] key) : base(key)
        {
            _key = new AesKey(key);

            InitializeStateMatrix();
        }

        public AES(IAesKey key) : base(key.RawBytes)
        {
            _key = key;

            InitializeStateMatrix();
        }

        private void InitializeStateMatrix()
        {
            _stateMatrix = new byte[4][];

            for (int i = 0; i < 4; i++)
                _stateMatrix[i] = new byte[4];
        }

        public override byte[] Encrypt(byte[] block)
        {
            base.Encrypt(block);
            _block = block;

            ReadNextBlock();
            EncryptNextBlock();
            WriteStateMatrix();

            return _block;
        }

        private void EncryptNextBlock()
        {
            RoundZero();
            RepeatRounds();
            FinalRound();
        }

        private void RepeatRounds()
        {
            for (int i = 0; i < _key.RoundsCount; i++)
                Round();
        }

        private void RoundZero()
        {
            byte[][] roundKey = _key.NextSubKey();
            _stateMatrix = AESFunction.AddRoundKey(_stateMatrix, roundKey);
        }

        private void Round()
        {
            _stateMatrix = AESFunction.SubBytes(_stateMatrix);
            _stateMatrix = AESFunction.ShiftRows(_stateMatrix);
            _stateMatrix = AESFunction.MixColumns(_stateMatrix);
            byte[][] roundKey = _key.NextSubKey();
            _stateMatrix = AESFunction.AddRoundKey(_stateMatrix, roundKey);
        }

        private void FinalRound()
        {
            _stateMatrix = AESFunction.SubBytes(_stateMatrix);
            _stateMatrix = AESFunction.ShiftRows(_stateMatrix);
            byte[][] roundKey = _key.NextSubKey();
            _stateMatrix = AESFunction.AddRoundKey(_stateMatrix, roundKey);
        }

        private void WriteStateMatrix()
        {
            int index = 0;
            for (int i = 0; i < _stateMatrix.Length; i++)
                for (int j = 0; j < _stateMatrix[i].Length; j++)
                    _block[index++] = _stateMatrix[j][i];
        }

        public override byte[] Decrypt(byte[] block)
        {
            base.Decrypt(block);
            _block = block;

            ReadNextBlock();
            DecryptNextBlock();
            WriteStateMatrix();

            return _block;
        }

        private void DecryptNextBlock()
        {
            ReverseFinalRound();
            RepeatDecryptRounds();
            ReverseRoundZero();
        }

        private void ReadNextBlock()
        {
            int index = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    _stateMatrix[j][i] = _block[index++];
        }

        private void ReverseRoundZero()
        {
            byte[][] roundKey = _key.PreviousSubKey();
            _stateMatrix = AESFunction.AddRoundKey(_stateMatrix, roundKey);
        }

        private void RepeatDecryptRounds()
        {
            for (int i = 0; i < _key.RoundsCount; i++)
                DecryptRound();
        }

        private void DecryptRound()
        {
            byte[][] roundKey = _key.PreviousSubKey();
            _stateMatrix = AESFunction.AddRoundKey(_stateMatrix, roundKey);
            _stateMatrix = AESFunction.ReverseMixColumns(_stateMatrix);
            _stateMatrix = AESFunction.ReverseShiftRows(_stateMatrix);
            _stateMatrix = AESFunction.ReverseSubBytes(_stateMatrix);
        }

        private void ReverseFinalRound()
        {
            byte[][] roundKey = _key.PreviousSubKey();
            _stateMatrix = AESFunction.AddRoundKey(_stateMatrix, roundKey);
            _stateMatrix = AESFunction.ReverseShiftRows(_stateMatrix);
            _stateMatrix = AESFunction.ReverseSubBytes(_stateMatrix);
        }
    }
}