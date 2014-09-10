using System;
using System.IO;

namespace MiniBinaryParser
{
    public class Symbol
    {
        public Symbol(Func<BinaryReader, bool> parse)
        {
            Parse = parse;
        }

        public Func<BinaryReader, bool> Parse { get; private set; }

        public static implicit operator Symbol(byte val)
        {
            return new Symbol((stream) => { return stream.ReadByte() == val; });
        }

        public static implicit operator Symbol(short val)
        {
            return new Symbol((stream) => { return stream.ReadInt16() == val; });
        }

        public static implicit operator Symbol(int val)
        {
            return new Symbol((stream) => { return stream.ReadInt32() == val; });
        }

        public static implicit operator Symbol(long val)
        {
            return new Symbol((stream) => { return stream.ReadInt64() == val; });
        }

        public static implicit operator Symbol(ushort val)
        {
            return new Symbol((stream) => { return stream.ReadUInt16() == val; });
        }

        public static implicit operator Symbol(uint val)
        {
            return new Symbol((stream) => { return stream.ReadUInt32() == val; });
        }

        public static implicit operator Symbol(ulong val)
        {
            return new Symbol((stream) => { return stream.ReadUInt64() == val; });
        }

        public static implicit operator Symbol(float val)
        {
            return new Symbol((stream) => { return stream.ReadSingle() == val; });
        }

        public static implicit operator Symbol(double val)
        {
            return new Symbol((stream) => { return stream.ReadDouble() == val; });
        }
    }
}
