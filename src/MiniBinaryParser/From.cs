using System;

namespace MiniBinaryParser
{
    public static partial class From
    {
        public static Symbol Byte(Action<byte> match)
        {
            return new Symbol((reader) => { match(reader.ReadByte()); return true; });
        }

        public static Symbol Bytes(int count, Action<byte[]> match)
        {
            return new Symbol((reader) => { match(reader.ReadBytes(count)); return true; });
        }

        public static Symbol Bytes(Func<int> count, Action<byte[]> match)
        {
            return new Symbol((reader) => { match(reader.ReadBytes(count())); return true; });
        }

        public static Symbol Int16(Action<short> match)
        {
            return new Symbol((reader) => { match(reader.ReadInt16()); return true; });
        }

        public static Symbol Int32(Action<int> match)
        {
            return new Symbol((reader) => { match(reader.ReadInt32()); return true; });
        }

        public static Symbol Int64(Action<long> match)
        {
            return new Symbol((reader) => { match(reader.ReadInt64()); return true; });
        }

        public static Symbol UInt16(Action<ushort> match)
        {
            return new Symbol((reader) => { match(reader.ReadUInt16()); return true; });
        }

        public static Symbol UInt32(Action<uint> match)
        {
            return new Symbol((reader) => { match(reader.ReadUInt32()); return true; });
        }

        public static Symbol UInt64(Action<ulong> match)
        {
            return new Symbol((reader) => { match(reader.ReadUInt64()); return true; });
        }

        public static Symbol EpochTimestamp(Action<DateTime> match)
        {
            return new Symbol((reader) =>
            {
                var read = reader.ReadUInt32();
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                date = date.AddSeconds(read);
                match(date);
                return true;
            });
        }

        public static Symbol Guid(Action<Guid> match)
        {
            return new Symbol((reader) =>
            {
                // Guid must be read in big endian according to RFC 4122...
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
