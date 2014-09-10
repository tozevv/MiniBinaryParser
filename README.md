MiniBinaryParser
================

Using
-----

Very simple binary parser for C#. Matches sequences in byte arrays:

```csharp
    byte[] sequence = new byte[] { 0x00, 0x01, 0x02, 0x03  };
    Match match = sequence.Parse(Endian.Big, (byte)0x01, (byte)0x02);

    byte[] matchedSequence = match.MatchedBytes;     // match sequence is    { 0x01, 0x02 }
    byte[] unmatchedSequence = match.UnmatchedBytes; // unmatche sequence is { 0x00, 0x03 }
```

The matching is not limited to sequences of bytes as bytes can represent *symbols* that come in two flavors:

   - *Constant Symbols*: typically used to match types of values or byte markers, usually byte or integer types
   - *Variable Symbols*: actual data to be retrieved from the binary sequence 

Here's a simple [Type-Length-Value](http://en.wikipedia.org/wiki/Type-length-value) parse, where type is a Constant Symbol and length and value ar Variable Symbols. 

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

The Parse function receives an array of symbols. An endianness parameter is required to remove any ambiguity when parsing integer and numeric types. Symbols are strongly typed. Current version supports, besides bytes, Unix Epoch style dates, RFC4122 UUID/GUID and all CLR integer types symbols.


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


Extending
---------

Support for new Symbols is possible by extending the "From" partial class or roll your own that generates a Symbol Parser. A symbol parser is just a ```Func<BinaryReader, bool> ``` receiving the byte reader at the current position and returning true if it's a match of false otherwise.

For example supporting reading booleans where 0x01 is true and everything else is false could be added as:

```csharp
    public class BooleanSymbol: Symbol
    {
        public BooleanSymbol(Func<BinaryReader, bool> parse): base(parse) {}
        public static Symbol Variable(Action<bool> match)
        {
            return new Symbol((reader) => { match(reader.ReadByte() == 1); return true; });
        }

        public static Symbol Const(bool val)
        {
            return new BooleanSymbol((reader) => { return (reader.ReadByte() == 1) == val; });
        }
    }
    
    ...
    
    byte[] sequence = new byte[] { 
         0x01, 0x00, 0x01, 0x00
    };

    bool isTrue = false, isFalse = true;

    Match m = sequence.Parse(Endian.Big,
        BooleanSymbol.Const(true),
        BooleanSymbol.Const(false),
        BooleanSymbol.Variable((s) => isTrue = s),
        BooleanSymbol.Variable((s) => isFalse = s)
    );
```





