using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBinaryParser
{
    public static class ByteArrayExtensions
    {
        public static Match Parse(this byte[] sequence, Endian endian, params Token[] pattern)
        {
            Match match = new Match();
            int patternPosition = 0;
            MemoryStream stream = new MemoryStream(sequence);

            BinaryReader br = new CustomEndianBinaryReader(endian, stream);

            while (br.BaseStream.CanRead && patternPosition < pattern.Length)
            {
                if (pattern[patternPosition].Parse(br))
                {
                    patternPosition++;
                    match.Start = (int)stream.Position;
                }
                else
                {
                    match.Start = 0;
                    patternPosition = 0;
                }
            }

            if (patternPosition == pattern.Length)
            {
                match.End = (int)stream.Position;
                match.MatchedBytes = sequence.Skip(match.Start).Take(match.End - match.Start).ToArray();
                match.MatchedBytes = sequence.Take(match.Start).Concat(sequence.Skip(match.End).Take(match.End - sequence.Length)).ToArray();
            } 
            else  
            {
                match.End = 0;
                match.MatchedBytes = null;
                match.UnmatchedBytes = sequence.ToArray();
            }

            return null;
        }
    }
}
