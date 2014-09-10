MiniBinaryParser
================

Very simple binary parser for C#.
Matches sequences in byte arrays:

```csharp
    byte[] sequence = new byte[] { 0x00, 0x01, 0x02, 0x03  };
    Match match = sequence.Parse(Endian.Big, (byte)0x01, (byte)0x02);

    byte[] matchedSequence = match.MatchedBytes;     // match sequence is    { 0x01, 0x02 }
    byte[] unmatchedSequence = match.UnmatchedBytes; // unmatche sequence is { 0x00, 0x03 }
```


And retrieves data from sequences using a very flexible strategy. Here's a simple Type-Length-Value parse:

```csharp
    byte[] sequence = new byte[] { 0x00, 0x00, 0xAA, 0xAF, 0x00, 0x04, 0x01, 0x02, 0x03, 0x04, 0x05  };
    short length = 0;
    byte[] data = null;
 
    Match m = sequence.Parse(Endian.Big, 
                0x0000AAAF,                                 // type
                From.Int16((len) => length = len),          // length
                From.Bytes(() => length, (bt) => data = bt) // value
                );

    // data is now 0x01, 0x02, 0x03, 0x04;
```

The "type" match rely in matching the sequence as a byte[] or as any integer type sequence honouring the Endianness parameter.

"From" data type parsers come in several flavours. Current version supports Unix Epoch style dates, RFC4122 UUID/GUID and all CLR integer types. Here's a more complex example showing 3 different data types and a sequecne match using ```unsigned short```.  


```csharp
    byte[] sequence = new byte[] { 
        0xFF, 0xAF,                                                                                     // Type 16Bit Int
        0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF, // Guid RFC4122
        0x00, 0x00, 0x01, 0x20,                                                                         // Unsigned int
        0x0D, 0x41, 0x75, 0x00                                                                          // Unix Epoch DateTime 
    };
    User user = new User();
    Match m = sequence.Parse(Endian.Big,
       (ushort)0xFFAF,                                                  // Type 16Bit Int
        From.Guid((userId) => user.UserId = userId),                    // Guid RFC4122
        From.UInt32((friends) => user.NumberOfFriends = friends),       // Unsigned int
        From.EpochTimestamp((birthDate) => user.BirthDate = birthDate)  // Unix Epoch DateTime
    );

    // user.UserId is "a0a1a2a3-a4a5-a6a7-a8a9-aaabacadaeaf"
    // user.NumberOfFriends is 288
    // user.BirthDate is 1977/01/18

```







