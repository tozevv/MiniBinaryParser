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
        public void TestReadDouble()
        {
            // Arrance
            byte[] sequence = new byte[] { 0xFF, 0xF0, 0x30, 0x40, 0x10, 0x20, 0x30, 0x40 };
            double bigEndian = 0;
            double littleEndian = 0;

            // Act
            sequence.Parse(Endian.Big, From.Double((s) => bigEndian = s));
            sequence.Parse(Endian.Little, From.Double((s) => littleEndian = s));

            // Assert
            Assert.AreEqual(bigEndian, 0x0102030405060708);
            Assert.AreEqual(littleEndian, 0x0807060504030201);
        }



        [TestMethod]
        public void TestReadInt()
        {
            
        }
    }
}
