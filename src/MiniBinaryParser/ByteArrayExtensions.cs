using System.IO;
using System.Linq;

namespace MiniBinaryParser
{
    public static class ByteArrayExtensions
    {
        public static Match Parse(this byte[] sequence, Endian endian, params Symbol[] symbols)
        {
            Match match = new Match();
            int patternPosition = 0;
            MemoryStream stream = new MemoryStream(sequence);

            BinaryReader br = new CustomEndianBinaryReader(endian, stream);

            while (br.BaseStream.CanRead && patternPosition < symbols.Length)
            {
                long pos = stream.Position;
                if (symbols[patternPosition].Parse(br))
                {
                    patternPosition++;
                    if (match.Start == 0)
                    {
                        match.Start = (int)pos;
                    }
                }
                else
                {
                    stream.Position = pos + 1; // failed to match, advance position only 1 byte
                    match.Start = 0;
                    patternPosition = 0;
                }
            }

            if (patternPosition == symbols.Length)
            {
                match.End = (int)stream.Position;
                match.MatchedBytes = sequence.Skip(match.Start).Take(match.End - match.Start).ToArray();
                match.UnmatchedBytes = sequence.Take(match.Start).Concat(sequence.Skip(match.End).Take(sequence.Length - match.End)).ToArray();
            } 
            else  
            {
                match.End = 0;
                match.MatchedBytes = null;
                match.UnmatchedBytes = sequence.ToArray();
            }
            return match;
        }
    }
}
