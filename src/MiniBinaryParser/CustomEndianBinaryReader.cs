using System;
using System.Linq;
using System.IO;

namespace MiniBinaryParser
{
    public class CustomEndianBinaryReader : BinaryReader
    {
        private readonly bool swapEndian = false;

        public CustomEndianBinaryReader(Endian endian, Stream s)
            : base(s)
        {
            this.swapEndian = BitConverter.IsLittleEndian ? endian == Endian.Big : endian == Endian.Little;
        }

        public override short ReadInt16()
        {
            return this.swapEndian ?
                BitConverter.ToInt16(Reverse(2), 0) :
                base.ReadInt16();
        }

        public override ushort ReadUInt16()
        {
            return this.swapEndian ?
                BitConverter.ToUInt16(Reverse(2), 0) :
                base.ReadUInt16();
        }

        public override int ReadInt32()
        {
            return this.swapEndian ?
                BitConverter.ToInt32(Reverse(4), 0) :
                base.ReadInt32();
        }

        public override uint ReadUInt32()
        {
            return this.swapEndian ?
                BitConverter.ToUInt32(Reverse(4), 0) :
                base.ReadUInt32();
        }

        public override long ReadInt64()
        {
            return this.swapEndian ?
                BitConverter.ToInt64(Reverse(8), 0) :
                base.ReadInt64();
        }

        public override ulong ReadUInt64()
        {
            return this.swapEndian ?
                BitConverter.ToUInt64(Reverse(8), 0) :
                base.ReadUInt64();
        }

        public override decimal ReadDecimal()
        {
            throw new NotImplementedException();
        }

        public override float ReadSingle()
        {
            return this.swapEndian ?
                BitConverter.ToSingle(Reverse(4), 0) :
                base.ReadSingle();
        }

        public override double ReadDouble()
        {
            return this.swapEndian ?
                BitConverter.ToDouble(Reverse(8), 0) :
                base.ReadDouble();
        }

        private byte[] Reverse(int count)
        {
            return base.ReadBytes(count).Reverse().ToArray();
        }
    }
}
