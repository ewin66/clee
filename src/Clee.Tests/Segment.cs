namespace Clee.Tests
{
    public class Segment
    {
        public Segment(string value, int beginOffset)
        {
            Value = value;
            BeginOffset = beginOffset;
        }

        public string Value { get; private set; }
        public int BeginOffset { get; private set; }

        public int EndOffset
        {
            get { return BeginOffset + Value.Length; }
        }
    }
}