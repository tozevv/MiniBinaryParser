namespace MiniBinaryParser
{
    public class Match
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Count
        {
            get
            {
                return MatchedBytes.Length;
            }
        }
        public byte[] MatchedBytes { get; set; }
        public byte[] UnmatchedBytes { get; set; }
    }
}
