using System;
using System.IO;

namespace MiniBinaryParser
{
    public class BigEndianBinaryReader: BinaryReader
    {
        public BigEndianBinaryReader(Stream s) : base(s) { }

        public BigEndianBinaryReader(BinaryReader r) : base(r.BaseStream) { }

        public override short ReadInt16()
        {
            var bytes = base.ReadBytes(2);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        public override ushort ReadUInt16()
        {
            var bytes = base.ReadBytes(2);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        public override int ReadInt32()
        {
            var bytes = base.ReadBytes(4);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public override uint ReadUInt32()
        {
            var bytes = base.ReadBytes(4);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public override long ReadInt64()
        {
            var bytes = base.ReadBytes(8);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        public override ulong ReadUInt64()
        {
            var bytes = base.ReadBytes(8);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        public override decimal ReadDecimal()
        {
            throw new NotImplementedException();
        }

        public override float ReadSingle()
        {
            var bytes = base.ReadBytes(4);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        public override double ReadDouble()
        {
            var bytes = base.ReadBytes(8);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }
    }
}
