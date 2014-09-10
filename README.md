MiniBinaryParser
================

Very simple binary parser for C#.
Matches sequences in byte arrays:

    byte[] sequence = new byte[] { 0x00, 0x01, 0x02, 0x03  };
    Match match = sequence.Parse(Endian.Big, (byte)0x01, (byte)0x02);

    byte[] matchedSequence = match.MatchedBytes;     // match sequence is    { 0x01, 0x02 }
    byte[] unmatchedSequence = match.UnmatchedBytes; // unmatche sequence is { 0x00, 0x03 }
   
