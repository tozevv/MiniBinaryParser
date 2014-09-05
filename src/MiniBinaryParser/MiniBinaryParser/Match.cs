
namespace MiniBinaryParser
{
    public class Match
    {
        public int Start { get; set; }
        public int End { get; set; }
        public byte[] MatchedBytes { get; set; }
        public byte[] UnmatchedBytes { get; set; }
    }
}
