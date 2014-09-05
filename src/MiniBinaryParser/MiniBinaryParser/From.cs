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
            return new Token((reader) => { match(reader.ReadByte()); return true; });
        }

        public static Token Bytes(Action<byte[]> match, int count)
        {
            return new Token((reader) => { match(reader.ReadBytes(count)); return true; });
        }

        public static Token Int16(Action<short> match)
        {
            return new Token((reader) => { match(reader.ReadInt16()); return true; });
        }

        public static Token Int32(Action<int> match)
        {
            return new Token((reader) => { match(reader.ReadInt32()); return true; });
        }

        public static Token Int64(Action<long> match)
        {
            return new Token((reader) => { match(reader.ReadInt64()); return true; });
        }

        public static Token Int16(Action<ushort> match)
        {
            return new Token((reader) => { match(reader.ReadUInt16()); return true; });
        }

        public static Token Int32(Action<uint> match)
        {
            return new Token((reader) => { match(reader.ReadUInt32()); return true; });
        }

        public static Token Int64(Action<ulong> match)
        {
            return new Token((reader) => { match(reader.ReadUInt64()); return true; });
        }

        public static Token EpochTimestamp(Action<DateTime> match)
        {
            return new Token((reader) =>
            {
                var read = reader.ReadUInt32();
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                date = date.AddSeconds(read);
                match(date);
                return true;
            });
        }

        public static Token Guid(Action<Guid> match)
        {
            return new Token((reader) =>
            {
                // Guid must be read in big endian...
                var bigEndianReader = new CustomEndianBinaryReader(Endian.Big, reader.BaseStream);
                Guid g = new Guid(bigEndianReader.ReadInt32(),
                         bigEndianReader.ReadInt16(),
                         bigEndianReader.ReadInt16(),
                         bigEndianReader.ReadByte(), bigEndianReader.ReadByte(), bigEndianReader.ReadByte(), bigEndianReader.ReadByte(),
                         bigEndianReader.ReadByte(), bigEndianReader.ReadByte(), bigEndianReader.ReadByte(), bigEndianReader.ReadByte());
                match(g);
                return true;
            });
        }
    }
}
