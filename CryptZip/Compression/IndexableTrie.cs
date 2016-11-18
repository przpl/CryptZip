using System.Collections.Generic;

namespace CryptZip.Compression
{
    public class IndexableTrie : Trie
    {
        private readonly List<TrieNode> _nodes;

        public IndexableTrie()
        {
            _nodes = new List<TrieNode>();
        }

        protected override void Add(TrieNode parent, TrieNode child)
        {
            _nodes.Add(child);
            base.Add(parent, child);
        }

        public TrieNode this[int index] => _nodes[index - 1];
    }
}