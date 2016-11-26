using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CryptZip.Compression
{
    public class LZW : ICompressor
    {
        private BitWriter _bitWriter;
        private BitReader _bitReader;
        private Trie _trie;
        private IndexableTrie _indexableTrie;
        private Stream _input, _output;
        private byte _next;

        public void Compress(Stream input, Stream output)
        {
            _input = input;
            _output = output;

            _bitWriter = new BitWriter(_output);
            _trie = new Trie();

            BuildTrie(_trie);

            _next = Convert.ToByte(_input.ReadByte());

            while (DataNotEnded())
                CompressNextString();

            Finalize();
        }

        private void BuildTrie(Trie trie)
        {
            for (int i = 0; i < 256; i++)
                trie.Add(Convert.ToByte(i));
        }

        private bool DataNotEnded()
        {
            return _input.Position < _input.Length;
        }

        private void CompressNextString()
        {
            TrieNode node = _trie.FindRootChild(_next);
            TrieNode lastNode = null;

            while (!Trie.IsRoot(node) && DataNotEnded())
            {
                _next = Convert.ToByte(_input.ReadByte());
                lastNode = node;
                node = _trie.FindChild(node, _next);
            }

            WriteToken(lastNode, _next);
        }

        private void Finalize()
        {
            WriteLastSymbol();
            _bitWriter.Flush();
            _trie.Dispose();
        }

        private void WriteLastSymbol()
        {
            _bitWriter.Write(BitConverter.ToBits(_next, 8));
        }

        private void WriteToken(TrieNode lastNode, byte symbol)
        {
            _trie.Add(lastNode, symbol);
            _bitWriter.Write(BitConverter.ToBits(BitConverter.MinimalNumberOfBits(lastNode.Index) - 1, 5));
            _bitWriter.Write(BitConverter.ToBits(lastNode.Index));
        }

        public void Decompress(Stream input, Stream output)
        {
            _input = input;
            _output = output;

            _indexableTrie = new IndexableTrie();
            _bitReader = new BitReader(_input);

            BuildTrie(_indexableTrie);

            int next = ReadToken();
            int last = next;

            while (_bitReader.BytesLeft > 1)
            {
                next = ReadToken();

                var lastNode = _indexableTrie[last];

                byte symbolFromTrie;
                if (next > _indexableTrie.Count)
                    symbolFromTrie = GetSymbol(_indexableTrie[last]);
                else 
                    symbolFromTrie = GetSymbol(_indexableTrie[next]);
                _indexableTrie.Add(lastNode, symbolFromTrie);

                ReadString(_indexableTrie[_indexableTrie.Count].Parent);

                last = next;
            }

            ReadLastToken(next);

            _output.Flush();
        }

        private void ReadLastToken(int next)
        {
            ReadString(_indexableTrie[next].Parent);
            _output.WriteByte(_indexableTrie[next].Value);
            _output.WriteByte(BitConverter.ToByte(_bitReader.Read(8)));
        }

        private int ReadToken()
        {
            int bitsPerToken = BitConverter.ToInt(_bitReader.Read(5)) + 1;
            return BitConverter.ToInt(_bitReader.Read(bitsPerToken));
        }

        private void ReadString(TrieNode node)
        {
            var stack = new Stack<byte>();
            while (!Trie.IsRoot(node))
            {
                stack.Push(node.Value);
                node = node.Parent;
            }

            while (stack.Any())
                _output.WriteByte(stack.Pop());
        }

        private byte GetSymbol(TrieNode node)
        {
            while (!Trie.IsRoot(node.Parent))
                node = node.Parent;

            return node.Value;
        }
    }
}