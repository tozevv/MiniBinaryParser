using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBinaryParser
{
    public static partial class From
    {
        public static Token Byte(Action<byte> match)
        {
            return new Token((stream) => { match(stream.ReadByte()); return true; });
        }

        public static Token Bytes(Action<byte[]> match, int count)
        {
            return new Token((stream) => { match(stream.ReadBytes(count)); return true; });
        }

        public static Token Int16(Action<short> match)
        {
            return new Token((stream) => { match(stream.ReadInt16()); return true; });
        }

        public static Token Int32(Action<int> match)
        {
            return new Token((stream) => { match(stream.ReadInt32()); return true; });
        }

        public static Token Int64(Action<long> match)
        {
            return new Token((stream) => { match(stream.ReadInt64()); return true; });
        }

        public static Token Int16(Action<ushort> match)
        {
            return new Token((stream) => { match(stream.ReadUInt16()); return true; });
        }

        public static Token Int32(Action<uint> match)
        {
            return new Token((stream) => { match(stream.ReadUInt32()); return true; });
        }

        public static Token Int64(Action<ulong> match)
        {
            return new Token((stream) => { match(stream.ReadUInt64()); return true; });
        }

        public static Token Single(Action<float> match)
        {
            return new Token((stream) => { match(stream.ReadSingle()); return true; });
        }

        public static Token Double(Action<double> match)
        {
            return new Token((stream) => { match(stream.ReadDouble()); return true; });
        }

        public static Token UnixTime(Action<DateTime> match)
        {
            return new Token((stream) =>
            {
                var read = stream.ReadInt32();
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                match(date);
                return true;
            });
        }

        public static Token Guid(Action<Guid> match)
        {
            return new Token((stream) =>
            {
                var read = stream.ReadInt32();
                Guid g = new Guid(stream.ReadInt32(),
                         stream.ReadInt16(),
                         stream.ReadInt16(),
                         stream.ReadByte(), stream.ReadByte(), stream.ReadByte(), stream.ReadByte(),
                         stream.ReadByte(), stream.ReadByte(), stream.ReadByte(), stream.ReadByte());
                match(g);
                return true;
            });
        }
    }
}
