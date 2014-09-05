using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MiniBinaryParser.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void TestSimpleSequence()
        {
            // Arrance
            byte[] testData = new byte[] {
                0x01,0x02,0x03,0x04,0x00,0x05,0x00,0x06
            };

            // Act

            // Assert
        }

        [TestMethod]
        public void TestAllFormatters()
        {
        }
    }
}
