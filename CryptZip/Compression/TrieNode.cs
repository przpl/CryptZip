using System.Collections.Generic;

namespace CryptZip.Compression
{
    public class TrieNode
    {
        public int Index { get; set; }
        public byte Value { get; set; }
        public TrieNode Parent { get; set; }
        public List<TrieNode> Children { get; set; }

        public void Clear()
        {
            Children?.Clear();
        }
    }
}