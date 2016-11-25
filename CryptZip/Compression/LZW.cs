using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            Finalize();
        }

        private void BuildTrie()
        {
            //for (byte i = 0; i < Byte.MaxValue; i++)
            //    _trie.Add(i);
            _trie.Add(1);
            _trie.Add(2);
            _trie.Add(3);
            _trie.Add(4);
            _trie.Add(5);
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
        }

        private void Finalize()
        {
            _bitWriter.Flush();
            _trie.Dispose();
        }

        private void WriteToken(TrieNode lastNode, byte symbol)
        {
            _trie.Add(lastNode, symbol);
            //_bitWriter.Write(BitConverter.ToBits(BitConverter.MinimalNumberOfBits(lastNode.Index) - 1, 5));
            _bitWriter.Write(BitConverter.ToBits(lastNode.Index, 8)); // , 8 tylko do testów
            Debug.WriteLine(lastNode.Index);
        }

        public void Decompress(Stream input, Stream output)
        {
            _input = input;
            _output = output;

            _indexableTrie = new IndexableTrie();
            var bitReader = new BitReader(_input);

            _indexableTrie.Add(1);
            _indexableTrie.Add(2);
            _indexableTrie.Add(3);
            _indexableTrie.Add(4);
            _indexableTrie.Add(5);

            //while (bitReader.BytesLeft > 1)
            //{
            //    int bitsPerIndex = BitConverter.ToInt(bitReader.Read(5)) + 1;
            //    int index = BitConverter.ToInt(bitReader.Read(bitsPerIndex));
            //    byte symbol = BitConverter.ToByte(bitReader.Read(8));

            //    if (IsOneCharString(index))
            //        ReadSymbol(symbol);
            //    else
            //        ReadSymbols(index, symbol);
            //}

            byte next = Convert.ToByte(_input.ReadByte());
            byte last = next;

            while (DataNotEnded())
            {
                next = Convert.ToByte(_input.ReadByte());
                if (next < 6) // < 256
                {
                    var node = _indexableTrie[last];
                    _indexableTrie.Add(node, next);
                    _output.WriteByte(next);
                }
                else
                {
                    var node = _indexableTrie[last];
                    byte symbolFromTrie = GetSymbol(_indexableTrie[next]);
                    _indexableTrie.Add(node, symbolFromTrie);
                    
                    // odczyt od najwyższego poziomu do danego węzła i wypisanie na wyjściu
                    GetBytes(node);
                }
                last = next;
            }

            _output.Flush();
        }

        private void GetBytes(TrieNode node)
        {
            var stack = new Stack<byte>();
            while (node.Parent.Index != 0) // funkcja is root
            {
                stack.Push(node.Value);
                node = node.Parent;
            }

            while (stack.Any())
            {
                _output.WriteByte(stack.Pop());
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
