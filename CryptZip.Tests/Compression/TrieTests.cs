﻿using CryptZip.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Compression
{
    [TestClass]
    public class TrieTests
    {
        [TestMethod]
        public void IndexOf_AddsOneSymbolElements_Added()
        {
            var trie = new Trie();

            trie.Add(0);
            trie.Add(1);
            trie.Add(2);

            Assert.AreEqual(1, trie.FindRootChild(0).Index);
            Assert.AreEqual(2, trie.FindRootChild(1).Index);
            Assert.AreEqual(3, trie.FindRootChild(2).Index);
        }

        [TestMethod]
        public void IndexOf_AddsTwoSymbolsElements_Added()
        {
            var trie = new Trie();
            trie.Add(0);
            trie.Add(1);
            trie.Add(2);

            TrieNode zeroNode = trie.FindRootChild(0);
            trie.Add(zeroNode, 0);
            Assert.AreEqual(4, trie.FindChild(zeroNode, 0).Index);

            TrieNode oneNode = trie.FindRootChild(1);
            trie.Add(oneNode, 1);
            Assert.AreEqual(5, trie.FindChild(oneNode, 1).Index);

            TrieNode twoNode = trie.FindRootChild(2);
            trie.Add(twoNode, 2);
            Assert.AreEqual(6, trie.FindChild(twoNode, 2).Index);
        }

        [TestMethod]
        public void Count_NoElementsAdded_Zero()
        {
            var trie = new Trie();

            Assert.AreEqual(0, trie.Count);
        }

        [TestMethod]
        public void Count_AddsThreeElements_Three()
        {
            var trie = new Trie();

            trie.Add(0);
            trie.Add(1);
            var node = trie.FindRootChild(1);
            trie.Add(node, 2);

            Assert.AreEqual(3, trie.Count);
        }
    }
}