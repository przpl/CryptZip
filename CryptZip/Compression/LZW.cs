using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CryptZip.Compression
{
    public class LZW : ICompressor
    {
        private BitWriter _bitWriter;
        private Trie _trie;
        private IndexableTrie _indexableTrie;
        private Stream _input, _output;
        private byte _next;

        private const int _bitsPerToken = 12;

        public void Compress(Stream input, Stream output)
        {
            _input = input;
            _output = output;

            _bitWriter = new BitWriter(_output);
            _trie = new Trie();

            BuildTrie();

            _next = Convert.ToByte(_input.ReadByte());

            while (DataNotEnded())
                CompressNextString();

            //debugStream.Close();

            Finalize();
        }

        private void BuildTrie()
        {
            for (int i = 0; i < 256; i++)
                _trie.Add(Convert.ToByte(i));
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

            if (lastNode != null && !Trie.IsRoot(lastNode))
                WriteToken(lastNode, _next);
            else
                WriteToken(node, _next);

            // zapis ostatniego znaku
            if (!DataNotEnded())
                _bitWriter.Write(BitConverter.ToBits(_next, 8));
        }

        private void Finalize()
        {
            _bitWriter.Flush();
            _trie.Dispose();
        }

        //private StreamWriter debugStream = new StreamWriter(@"E:\tokens.txt");

        private void WriteToken(TrieNode lastNode, byte symbol)
        {
            _trie.Add(lastNode, symbol);
            // liczba bitów na indeks, indeks (5 oznacza maksymalnie 32-bitowe indeksy)
            //_bitWriter.Write(BitConverter.ToBits(BitConverter.MinimalNumberOfBits(lastNode.Index), 5));
            _bitWriter.Write(BitConverter.ToBits(lastNode.Index, _bitsPerToken));
            //debugStream.WriteLine(lastNode.Index);
        }

        private BitReader _bitReader;
        private byte lastSymbol;

        public void Decompress(Stream input, Stream output)
        {
            _input = input;
            _output = output;

            _indexableTrie = new IndexableTrie();
            _bitReader = new BitReader(_input);

            // Build trie
            for (int i = 0; i < 256; i++)
                _indexableTrie.Add(Convert.ToByte(i));

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

                // odczyt od najwyższego poziomu do danego węzła i wypisanie na wyjściu
                GetBytes(_indexableTrie[_indexableTrie.Count].Parent);

                last = next;
            }

            GetBytes(_indexableTrie[next].Parent);
            _output.WriteByte(_indexableTrie[next].Value);
            //_output.WriteByte(_indexableTrie[_indexableTrie.Count].Value); // zamiastego odczytanie wartości ostatniego bajtu
            _output.WriteByte(BitConverter.ToByte(_bitReader.Read(8)));
            _output.Flush();
        }

        private int ReadToken()
        {
            int value = BitConverter.ToInt(_bitReader.Read(_bitsPerToken));
            return value;
        }

        private void GetBytes(TrieNode node)
        {
            var stack = new Stack<byte>();
            while (node.Index != 0) // funkcja is root
            {
                stack.Push(node.Value);
                node = node.Parent;
            }

            while (stack.Any())
            {
                byte value = stack.Pop();
                _output.WriteByte(value);
            }
        }

        private byte GetSymbol(TrieNode node)
        {
            while (node.Parent.Index != 0) // funkcja is root
                node = node.Parent;

            return node.Value;
        }

        //_next = Convert.ToByte(_input.ReadByte());
        //    _output.WriteByte(_next);

        //    while (DataNotEnded())
        //    {
        //        byte last = _next;
        //_next = Convert.ToByte(_input.ReadByte());
        //        _output.WriteByte(_next);

        //        var node = _indexableTrie[last];
        //_indexableTrie.Add(node, _next);
        //    }

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