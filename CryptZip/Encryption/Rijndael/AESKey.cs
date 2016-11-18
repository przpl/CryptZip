using System;
using System.Collections.Generic;

namespace CryptZip.Encryption.Rijndael
{
    public class AESKey
    {
        public int RoundsCount { get; private set; }
        public byte[] Bytes { get; }
        public int Length => Bytes.Length;

        private List<byte> _expandedKey;
        private int _keyIndex, _backwardKeyIndex;
        private readonly byte[][] _roundKey;

        public AESKey(byte[] key)
        {
            if (!IsValidKeyLength(key))
                throw new ArgumentException(nameof(key), "Key length has to be equal to 128 bits, 192 bits or 256 bits.");

            Bytes = Expand(key);
            _backwardKeyIndex = Bytes.Length - 1;
            _roundKey = new byte[4][];
            for (int i = 0; i < 4; i++)
                _roundKey[i] = new byte[4];

            SetRoundsCount();
        }

        private void SetRoundsCount()
        {
            switch (Bytes.Length)
            {
                case 176:
                    RoundsCount = 9;
                    break;
                case 208:
                    RoundsCount = 11;
                    break;
                case 240:
                    RoundsCount = 13;
                    break;
            }
        }

        private bool IsValidKeyLength(byte[] key)
        {
            return key.Length == 16 ||
                   key.Length == 24 ||
                   key.Length == 32;
        }

        private byte[] Expand(byte[] key)
        {
            int expandedKeyLength = LengthAfterExpansion(key.Length);
            _expandedKey = new List<byte>(key);

            int iteration = 1;

            while (_expandedKey.Count < expandedKeyLength)
            {
                byte[] lastFourBytes = _expandedKey.LastElements(4);
                lastFourBytes = lastFourBytes.ShiftToLeft();
                lastFourBytes = S_Box.Transform(lastFourBytes);
                Rcon.Apply(lastFourBytes, iteration);
                IEnumerable<byte> fourBytes = _expandedKey.GetRange(_expandedKey.Count - key.Length, 4);
                lastFourBytes = lastFourBytes.XOR(fourBytes);
                _expandedKey.AddRange(lastFourBytes);

                for (int i = 0; i < 3; i++)
                {
                    lastFourBytes = _expandedKey.LastElements(4);
                    fourBytes = _expandedKey.GetRange(_expandedKey.Count - key.Length, 4);
                    lastFourBytes = lastFourBytes.XOR(fourBytes);
                    _expandedKey.AddRange(lastFourBytes);
                }

                if (key.Length == 32 && _expandedKey.Count < 240)
                {
                    lastFourBytes = _expandedKey.LastElements(4);
                    lastFourBytes = S_Box.Transform(lastFourBytes);
                    fourBytes = _expandedKey.GetRange(_expandedKey.Count - key.Length, 4);
                    lastFourBytes = lastFourBytes.XOR(fourBytes);
                    _expandedKey.AddRange(lastFourBytes);
                }

                int repeat = 0;
                if (key.Length == 24 && _expandedKey.Count < 208)
                    repeat = 2;
                else if (key.Length == 32 && _expandedKey.Count < 240)
                    repeat = 3;

                for (int i = 0; i < repeat; i++)
                {
                    lastFourBytes = _expandedKey.LastElements(4);
                    fourBytes = _expandedKey.GetRange(_expandedKey.Count - key.Length, 4);
                    lastFourBytes = lastFourBytes.XOR(fourBytes);
                    _expandedKey.AddRange(lastFourBytes);
                }

                iteration++;
            }

            return _expandedKey.ToArray();
        }

        public byte[][] NextSubKey()
        {
            if (_keyIndex == Bytes.Length)
                _keyIndex = 0;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    _roundKey[j][i] = Bytes[_keyIndex++];

            return _roundKey;
        }

        public byte[][] PreviousSubKey()
        {
            if (_backwardKeyIndex == -1)
                _backwardKeyIndex = Bytes.Length - 1;

            for (int i = 3; i >= 0; i--)
                for (int j = 3; j >= 0; j--)
                    _roundKey[j][i] = Bytes[_backwardKeyIndex--];

            return _roundKey;
        }

        private int LengthAfterExpansion(int keyLength)
        {
            switch (keyLength)
            {
                case 16:
                    return 176;
                case 24:
                    return 208;
                case 32:
                    return 240;
                default:
                    throw new ArgumentOutOfRangeException(nameof(keyLength));
            }
        }

        public byte this[int index] => Bytes[index];
    }
}
