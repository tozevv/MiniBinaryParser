using System;
using MiniBinaryParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MiniBinaryParser.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void TestReadInt16()
        {
            // Arrance
            byte[] sequence = new byte[] { 0x00, 0x01 };
            short bigEndian = 0;
            short littleEndian = 0;

            // Act
            sequence.Parse(Endian.Big, From.Int16((s) => bigEndian = s));
            sequence.Parse(Endian.Little, From.Int16((s) => littleEndian = s));

            // Assert
            Assert.AreEqual(bigEndian, 0x0001);
            Assert.AreEqual(littleEndian, 0x0100);
        }

        [TestMethod]
        public void TestReadInt32()
        {
            // Arrance
            byte[] sequence = new byte[] { 0x01,0x02, 0x03, 0x04 };
            int bigEndian = 0;
            int littleEndian = 0;

            // Act
            sequence.Parse(Endian.Big, From.Int32((s) => bigEndian = s));
            sequence.Parse(Endian.Little, From.Int32((s) => littleEndian = s));

            // Assert
            Assert.AreEqual(bigEndian, 0x01020304);
            Assert.AreEqual(littleEndian, 0x04030201);
        }

        [TestMethod]
        public void TestReadInt64()
        {
            // Arrance
            byte[] sequence = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
            long bigEndian = 0;
            long littleEndian = 0;

            // Act
            sequence.Parse(Endian.Big, From.Int64((s) => bigEndian = s));
            sequence.Parse(Endian.Little, From.Int64((s) => littleEndian = s));

            // Assert
            Assert.AreEqual(bigEndian, 0x0102030405060708);
            Assert.AreEqual(littleEndian, 0x0807060504030201);
        }

        [TestMethod]
        public void TestEpochTimestamp()
        {
            // Arrance
            byte[] sequence = new byte[] { 236, 249, 9, 84 };
            DateTime bigEndian = DateTime.MinValue;
            DateTime littleEndian = DateTime.MinValue;

            // Act
            sequence.Parse(Endian.Big, From.EpochTimestamp((s) => bigEndian = s));
            sequence.Parse(Endian.Little, From.EpochTimestamp((s) => littleEndian = s));

            // Assert
            Assert.AreEqual(bigEndian, new DateTime(2095,12,26,13,17,08));
            Assert.AreEqual(littleEndian,  new DateTime(2014,09,05,17,59,08));
        }
    }
}
