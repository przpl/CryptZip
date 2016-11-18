using CryptZip.Encryption.SerpentAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Encryption.SerpentAlgorithms
{
    [TestClass]
    public class SerpentSboxTests
    {
        [TestMethod]
        public void Substitute_SubstitutesWordsSBox0_Substituted()
        {
            CollectionAssert.AreEqual(new uint[] {0xfff6cc3c, 0xfff61ff3, 0x00222115, 0x0028aefa}, new SerpentSbox().Substitute(621857, 2161724, 241647, 241672, 0));
        }

        [TestMethod]
        public void Substitute_SubstitutesWordsSbox1_Substituted()
        {
            CollectionAssert.AreEqual(new uint[] {0xffd5c2ce, 0xfff6cc38, 0xffdf6004, 0xffdd1ee2 }, new SerpentSbox().Substitute(621857, 2161724, 241647, 241672, 1));
        }

        [TestMethod]
        public void Substitute_SubstitutesWordsSbox2_Substituted()
        {
            CollectionAssert.AreEqual(new uint[] { 0x0021cefa, 0x00292ef6, 0x00289d39, 0xffd54d25 }, new SerpentSbox().Substitute(621857, 2161724, 241647, 241672, 2));
        }

        [TestMethod]
        public void Substitute_SubstitutesWordsSbox3_Substituted()
        {
            CollectionAssert.AreEqual(new uint[] { 0x00298d39, 0x002b2c34, 0x0009b2ce, 0x002a63d3 }, new SerpentSbox().Substitute(621857, 2161724, 241647, 241672, 3));
        }

        [TestMethod]
        public void Substitute_SubstitutesWordsSbox4_Substituted()
        {
            CollectionAssert.AreEqual(new uint[] { 0xffdee00c, 0x0009dc2c, 0x0009aeea, 0x002b32d6 }, new SerpentSbox().Substitute(621857, 2161724, 241647, 241672, 4));
        }

        [TestMethod]
        public void Substitute_SubstitutesWordsSbox5_Substituted()
        {
            CollectionAssert.AreEqual(new uint[] { 0xffdee00c, 0xfff5c119, 0xffdf8eea, 0xffd65d25 }, new SerpentSbox().Substitute(621857, 2161724, 241647, 241672, 5));
        }

        [TestMethod]
        public void Substitute_SubstitutesWordsSbox6_Substituted()
        {
            CollectionAssert.AreEqual(new uint[] { 0xffd75c20, 0xffdd9c2c, 0xfff65115, 0x00229efa }, new SerpentSbox().Substitute(621857, 2161724, 241647, 241672, 6));
        }

        [TestMethod]
        public void Substitute_SubstitutesWordsSbox7_Substituted()
        {
            CollectionAssert.AreEqual(new uint[] { 0xffff8c38, 0x00213ef6, 0x0028a2da, 0x002a1ff3 }, new SerpentSbox().Substitute(621857, 2161724, 241647, 241672, 7));
        }

        [TestMethod]
        public void Inverse_InversesWordsSubstitutionSBox0_Inversed()
        {
            CollectionAssert.AreEqual(new uint[] { 621857, 2161724, 241647, 241672 }, new SerpentSbox().Inverse(0xfff6cc3c, 0xfff61ff3, 0x00222115, 0x0028aefa, 0));
        }

        [TestMethod]
        public void Inverse_InversesWordsSubstitutionSBox1_Inversed()
        {
            CollectionAssert.AreEqual(new uint[] { 621857, 2161724, 241647, 241672 }, new SerpentSbox().Inverse(0xffd5c2ce, 0xfff6cc38, 0xffdf6004, 0xffdd1ee2, 1));
        }

        [TestMethod]
        public void Inverse_InversesWordsSubstitutionSBox2_Inversed()
        {
            CollectionAssert.AreEqual(new uint[] { 621857, 2161724, 241647, 241672 }, new SerpentSbox().Inverse(0x0021cefa, 0x00292ef6, 0x00289d39, 0xffd54d25, 2));
        }

        [TestMethod]
        public void Inverse_InversesWordsSubstitutionSBox3_Inversed()
        {
            CollectionAssert.AreEqual(new uint[] { 621857, 2161724, 241647, 241672 }, new SerpentSbox().Inverse(0x00298d39, 0x002b2c34, 0x0009b2ce, 0x002a63d3, 3));
        }

        [TestMethod]
        public void Inverse_InversesWordsSubstitutionSBox4_Inversed()
        {
            CollectionAssert.AreEqual(new uint[] { 621857, 2161724, 241647, 241672 }, new SerpentSbox().Inverse(0xffdee00c, 0x0009dc2c, 0x0009aeea, 0x002b32d6, 4));
        }

        [TestMethod]
        public void Inverse_InversesWordsSubstitutionSBox5_Inversed()
        {
            CollectionAssert.AreEqual(new uint[] { 621857, 2161724, 241647, 241672 }, new SerpentSbox().Inverse(0xffdee00c, 0xfff5c119, 0xffdf8eea, 0xffd65d25, 5));
        }

        [TestMethod]
        public void Inverse_InversesWordsSubstitutionSBox6_Inversed()
        {
            CollectionAssert.AreEqual(new uint[] { 621857, 2161724, 241647, 241672 }, new SerpentSbox().Inverse(0xffd75c20, 0xffdd9c2c, 0xfff65115, 0x00229efa, 6));
        }

        [TestMethod]
        public void Inverse_InversesWordsSubstitutionSBox7_Inversed()
        {
            CollectionAssert.AreEqual(new uint[] { 621857, 2161724, 241647, 241672 }, new SerpentSbox().Inverse(0xffff8c38, 0x00213ef6, 0x0028a2da, 0x002a1ff3, 7));
        }
    }
}