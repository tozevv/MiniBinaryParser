using System;
using System.IO;

namespace MiniBinaryParser
{
    public class Token
    {
        public Token(Func<BinaryReader, bool> parse)
        {
            Parse = parse;
        }

        public Func<BinaryReader, bool> Parse { get; private set; }

        public static implicit operator Token(byte val)
        {
            return new Token((stream) => { return stream.ReadByte() == val; });
        }

        public static implicit operator Token(short val)
        {
            return new Token((stream) => { return stream.ReadInt16() == val; });
        }

        public static implicit operator Token(int val)
        {
            return new Token((stream) => { return stream.ReadInt32() == val; });
        }

        public static implicit operator Token(long val)
        {
            return new Token((stream) => { return stream.ReadInt64() == val; });
        }

        public static implicit operator Token(ushort val)
        {
            return new Token((stream) => { return stream.ReadUInt16() == val; });
        }

        public static implicit operator Token(uint val)
        {
            return new Token((stream) => { return stream.ReadUInt32() == val; });
        }

        public static implicit operator Token(ulong val)
        {
            return new Token((stream) => { return stream.ReadUInt64() == val; });
        }

        public static implicit operator Token(float val)
        {
            return new Token((stream) => { return stream.ReadSingle() == val; });
        }

        public static implicit operator Token(double val)
        {
            return new Token((stream) => { return stream.ReadDouble() == val; });
        }

    }
}
