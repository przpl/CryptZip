using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public void LastElements_GetsFiveElements_Returned()
        {
            List<int> list = new List<int>(new [] {1,2,3,4,5,6,7,8,9});
            CollectionAssert.AreEqual(new [] {5,6,7,8,9}, list.LastElements(5));
        }

        [TestMethod]
        public void LastElements_GetsOneElemente_Returned()
        {
            List<int> list = new List<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            CollectionAssert.AreEqual(new [] { 9 }, list.LastElements(1));
        }

        [TestMethod]
        public void ShiftToLeft_ShiftsArrayToLeft_Shifted()
        {
            int[] array = {1, 2, 3, 4, 5};
            CollectionAssert.AreEqual(new [] {2,3,4,5,1}, array.ShiftToLeft());
        }

        [TestMethod]
        public void ShiftToLeft_ShiftsArrayToLeftThreeTimes_Shifted()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            CollectionAssert.AreEqual(new[] { 4, 5, 1, 2, 3 }, array.ShiftToLeft(3));
        }

        [TestMethod]
        public void ShiftToRight_ShiftsArrayToRight_Shifted()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            CollectionAssert.AreEqual(new[] { 5, 1, 2, 3, 4 }, array.ShiftToRight());
        }

        [TestMethod]
        public void ShiftToRight_ShiftsArrayToRightThreeTimes_Shifted()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            CollectionAssert.AreEqual(new[] { 3, 4, 5, 1, 2 }, array.ShiftToRight(3));
        }

        [TestMethod]
        public void XOR_PerformsExclusiveOr_Calculated()
        {
            byte[] array = { 1, 2, 3, 4, 5 };
            CollectionAssert.AreEqual(new byte[] { 51, 62, 69, 84, 120 }, array.XOR(new byte[]{50,60,70,80,125}));
        }

        [TestMethod]
        public void SubArray_GetsSubArray_GotSubArray()
        {
            byte[] array = { 1, 2, 3, 4, 5 };
            CollectionAssert.AreEqual(new byte[] { 2, 3 }, array.SubArray(1, 3));
        }
    }
}