using System;
using System.Collections.Generic;

namespace CryptZip.Compression
{
    public class Trie : IDisposable
    {
        public int Count => NextNodeIndex - 1; // without root

        protected int NextNodeIndex;

        private readonly TrieNode _root;

        public Trie()
        {
            _root = new TrieNode { Children = new List<TrieNode>() };
            NextNodeIndex = 1;
        }

        public void Add(byte value)
        {
            Add(_root, value);
        }

        public void Add(TrieNode parent, byte value)
        {
            var child = new TrieNode
            {
                Index = NextNodeIndex,
                Value = value,
                Parent = parent,
                Children = new List<TrieNode>()
            };
            Add(parent, child);
        }

        protected virtual void Add(TrieNode parent, TrieNode child)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent), "Cannot add child to null node.");

            parent.Children.Add(child);
            NextNodeIndex++;
        }

        public TrieNode FindRootChild(byte b)
        {
            return FindChild(_root, b);
        }

        public TrieNode GetRootChild(int index)
        {
            return _root.Children[index];
        }

        public TrieNode FindChild(TrieNode parent, byte value)
        {
            int index = parent.Children.FindIndex(n => n.Value == value);
            if (index >= 0)
                return parent.Children[index];
            return _root;
        }

        public void Dispose()
        {
            Clear(_root);
            NextNodeIndex = 1;
        }

        private void Clear(TrieNode node)
        {
            foreach (var child in node.Children)
                Clear(child);
            node.Clear();
        }

        public static bool IsRoot(TrieNode node)
        {
            return node.Index == 0;
        }
    }
}
