using System.Collections.Generic;
using System.IO;

namespace CryptZip.Compression
{
    public class LZ78 : ICompressor
    {
        private BitWriter _bitWriter;
        private Trie _trie;
        private IndexableTrie _indexableTrie;
        private Stream _input, _output;

        public void Compress(Stream input, Stream output)
        {
            _input = input;
            _output = output;

            _bitWriter = new BitWriter(_output);
            _trie = new Trie();

            while (DataNotEnded())
                CompressNextString();

            Finalize();
        }

        private bool DataNotEnded()
        {
            return _input.Position < _input.Length;
        }

        private void CompressNextString()
        {
            byte next = (byte)_input.ReadByte();

            TrieNode node = _trie.FindRootChild(next);
            TrieNode lastNode = null;

            while (!Trie.IsRoot(node) && DataNotEnded())
            {
                next = (byte)_input.ReadByte();
                lastNode = node;
                node = _trie.FindChild(node, next);
            }

            if (lastNode != null && !Trie.IsRoot(lastNode))
                WriteToken(lastNode, next);
            else
                WriteToken(node, next);
        }

        private void Finalize()
        {
            _bitWriter.Flush();
            _trie.Dispose();
        }

        private void WriteToken(TrieNode lastNode, byte symbol)
        {
            _trie.Add(lastNode, symbol);
            _bitWriter.Write(BitConverter.ToBits(BitConverter.MinimalNumberOfBits(lastNode.Index) - 1, 5));
            _bitWriter.Write(BitConverter.ToBits(lastNode.Index));
            _bitWriter.Write(BitConverter.ToBits(symbol, 8));
        }

        public void Decompress(Stream input, Stream output)
        {
            _input = input;
            _output = output;

            _indexableTrie = new IndexableTrie();
            var bitReader = new BitReader(_input);

            while (bitReader.BytesLeft > 1)
            {
                int bitsPerIndex = BitConverter.ToInt(bitReader.Read(5)) + 1;
                int index = BitConverter.ToInt(bitReader.Read(bitsPerIndex));
                byte symbol = BitConverter.ToByte(bitReader.Read(8));

                if (IsOneCharString(index))
                    ReadSymbol(symbol);
                else
                    ReadSymbols(index, symbol);
            }
        }

        private bool IsOneCharString(int index)
        {
            return index == 0;
        }

        private void ReadSymbol(byte symbol)
        {
            _indexableTrie.Add(symbol);
            _output.WriteByte(symbol);
        }

        private void ReadSymbols(int index, byte symbol)
        {
            var bytes = new Stack<byte>();
            bytes.Push(symbol);

            var node = _indexableTrie[index];
            _indexableTrie.Add(node, symbol);

            while (node.Index > 0)
            {
                bytes.Push(node.Value);
                node = node.Parent;
            }

            while (bytes.Count > 0)
                _output.WriteByte(bytes.Pop());
        }
    }
}