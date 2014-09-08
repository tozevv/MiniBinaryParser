using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MiniBinaryParser.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void TestReadInt16()
        {
            // Arrange
            byte[] sequence = new byte[] { 0x00, 0x01 };
            short bigEndian = 0;
            short littleEndian = 0;

            // Act
            sequence.Parse(Endian.Big, From.Int16((s) => bigEndian = s));
            sequence.Parse(Endian.Little, From.Int16((s) => littleEndian = s));

            // Assert
            Assert.AreEqual(0x0001, bigEndian);
            Assert.AreEqual(0x0100, littleEndian);
        }

        [TestMethod]
        public void TestReadInt32()
        {
            // Arrange
            byte[] sequence = new byte[] { 0x01,0x02, 0x03, 0x04 };
            int bigEndian = 0;
            int littleEndian = 0;

            // Act
            sequence.Parse(Endian.Big, From.Int32((s) => bigEndian = s));
            sequence.Parse(Endian.Little, From.Int32((s) => littleEndian = s));

            // Assert
            Assert.AreEqual(0x01020304, bigEndian);
            Assert.AreEqual(0x04030201, littleEndian);
        }

        [TestMethod]
        public void TestReadInt64()
        {
            // Arrange
            byte[] sequence = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
            long bigEndian = 0;
            long littleEndian = 0;

            // Act
            sequence.Parse(Endian.Big, From.Int64((s) => bigEndian = s));
            sequence.Parse(Endian.Little, From.Int64((s) => littleEndian = s));

            // Assert
            Assert.AreEqual(0x0102030405060708, bigEndian);
            Assert.AreEqual(0x0807060504030201, littleEndian);
        }

        [TestMethod]
        public void TestEpochTimestamp()
        {
            // Arrange
            byte[] sequence = new byte[] { 236, 249, 9, 84 };
            DateTime bigEndian = DateTime.MinValue;
            DateTime littleEndian = DateTime.MinValue;

            // Act
            sequence.Parse(Endian.Big, From.EpochTimestamp((s) => bigEndian = s));
            sequence.Parse(Endian.Little, From.EpochTimestamp((s) => littleEndian = s));

            // Assert
            Assert.AreEqual(new DateTime(2095,12,26,13,17,08), bigEndian);
            Assert.AreEqual(new DateTime(2014,09,05,17,59,08), littleEndian);
        }

        [TestMethod]
        public void TestGuid()
        {
        

            // Arrange
            byte[] sequence = new byte[] { 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF};
            Guid bigEndian = Guid.Empty;
            Guid littleEndian = Guid.Empty;

            // Act
            sequence.Parse(Endian.Big, From.Guid((s) => bigEndian = s));
            sequence.Parse(Endian.Little, From.Guid((s) => littleEndian = s));

            // Assert
            Assert.AreEqual(bigEndian, littleEndian); // should make no difference, RFC4122 states big endian always.
            Assert.AreEqual(Guid.Parse("a0a1a2a3-a4a5-a6a7-a8a9-aaabacadaeaf"), littleEndian);
        }

        [TestMethod]
        public void TestByteSequence()
        {
            // Arrange
            byte[] sequence = new byte[] { 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF };
            byte[] expectMatch = new byte[] { 0xA5, 0xA6 };
            byte[] expectUnmatch = new byte[] { 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF  };
            Guid bigEndian = Guid.Empty;
            Guid littleEndian = Guid.Empty;

            // Act
            var matchBig = sequence.Parse(Endian.Big, (byte)0xA5, (byte)0xA6);
            var matchLittle = sequence.Parse(Endian.Little, (byte)0xA5, (byte)0xA6);

            // Assert
            Assert.AreEqual(2, matchBig.Count);
            Assert.AreEqual(5, matchBig.Start);
            Assert.AreEqual(7, matchBig.End);
            CollectionAssert.AreEqual(expectMatch, matchBig.MatchedBytes);
            CollectionAssert.AreEqual(expectUnmatch, matchBig.UnmatchedBytes);

            Assert.AreEqual(2, matchLittle.Count);
            Assert.AreEqual(5, matchLittle.Start);
            Assert.AreEqual(7, matchLittle.End);
            CollectionAssert.AreEqual(expectMatch, matchLittle.MatchedBytes);
            CollectionAssert.AreEqual(expectUnmatch, matchLittle.UnmatchedBytes);
        }


        [TestMethod]
        public void TestUShortSequence()
        {
            // Arrange
            byte[] sequence = new byte[] { 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF };
            byte[] expectMatch = new byte[] { 0xA5, 0xA6 };
            byte[] expectUnmatch = new byte[] { 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF };
            Guid bigEndian = Guid.Empty;
            Guid littleEndian = Guid.Empty;

            // Act
            var matchBig = sequence.Parse(Endian.Big, (ushort)0xA5A6);
            var matchLittle = sequence.Parse(Endian.Little, (ushort)0xA6A5);

            // Assert
            Assert.AreEqual(2, matchBig.Count);
            Assert.AreEqual(5, matchBig.Start);
            Assert.AreEqual(7, matchBig.End);
            CollectionAssert.AreEqual(expectMatch, matchBig.MatchedBytes);
            CollectionAssert.AreEqual(expectUnmatch, matchBig.UnmatchedBytes);

            Assert.AreEqual(2, matchLittle.Count);
            Assert.AreEqual(5, matchLittle.Start);
            Assert.AreEqual(7, matchLittle.End);
            CollectionAssert.AreEqual(expectMatch, matchLittle.MatchedBytes);
            CollectionAssert.AreEqual(expectUnmatch, matchLittle.UnmatchedBytes);
        }

        [TestMethod]
        public void TestUIntSequence()
        {
            // Arrange
            byte[] sequence = new byte[] { 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF };
            byte[] expectMatch = new byte[] { 0xA5, 0xA6, 0xA7, 0xA8 };
            byte[] expectUnmatch = new byte[] { 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF };
            Guid bigEndian = Guid.Empty;
            Guid littleEndian = Guid.Empty;

            // Act
            var matchBig = sequence.Parse(Endian.Big, (uint)0xA5A6A7A8);
            var matchLittle = sequence.Parse(Endian.Little, (uint)0xA8A7A6A5);

            // Assert
            Assert.AreEqual(4, matchBig.Count);
            Assert.AreEqual(5, matchBig.Start);
            Assert.AreEqual(9, matchBig.End);
            CollectionAssert.AreEqual(expectMatch, matchBig.MatchedBytes);
            CollectionAssert.AreEqual(expectUnmatch, matchBig.UnmatchedBytes);

            Assert.AreEqual(4, matchLittle.Count);
            Assert.AreEqual(5, matchLittle.Start);
            Assert.AreEqual(9, matchLittle.End);
            CollectionAssert.AreEqual(expectMatch, matchLittle.MatchedBytes);
            CollectionAssert.AreEqual(expectUnmatch, matchLittle.UnmatchedBytes);
        }

        [TestMethod]
        public void TestULongSequence()
        {
            // Arrange
            byte[] sequence = new byte[] { 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF };
            byte[] expectMatch = new byte[] { 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC };
            byte[] expectUnmatch = new byte[] { 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xAD, 0xAE, 0xAF };
            Guid bigEndian = Guid.Empty;
            Guid littleEndian = Guid.Empty;

            // Act
            var matchBig = sequence.Parse(Endian.Big, (ulong)0xA5A6A7A8A9AAABAC);
            var matchLittle = sequence.Parse(Endian.Little, (ulong)0xACABAAA9A8A7A6A5);

            // Assert
            Assert.AreEqual(8, matchBig.Count);
            Assert.AreEqual(5, matchBig.Start);
            Assert.AreEqual(13, matchBig.End);
            CollectionAssert.AreEqual(expectMatch, matchBig.MatchedBytes);
            CollectionAssert.AreEqual(expectUnmatch, matchBig.UnmatchedBytes);

            Assert.AreEqual(8, matchLittle.Count);
            Assert.AreEqual(5, matchLittle.Start);
            Assert.AreEqual(13, matchLittle.End);
            CollectionAssert.AreEqual(expectMatch, matchLittle.MatchedBytes);
            CollectionAssert.AreEqual(expectUnmatch, matchLittle.UnmatchedBytes);
        }
    }
}