

namespace TimeLib
{
    public readonly struct Time
    {
        public byte Hours { get; }
        public byte Minutes { get; }
        public byte Seconds { get; }

        public Time(byte? hours = null, byte? minutes = null, byte? seconds = null)
        {
            if (hours == null) hours = 0;
            if (minutes == null) minutes = 0;
            if (seconds == null) seconds = 0;
            if (hours > 23 || minutes > 59 || seconds > 59) throw new ArgumentOutOfRangeException();

            Hours = (byte)hours;
            Minutes = (byte)minutes;
            Seconds = (byte)seconds;
        }
        public Time()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
        }
        public Time(string s) // for string in format hh:mm:ss or hh.mm.ss
        {
            char[] chars = { ':', '.' };
            string[] data = s.Split(chars, StringSplitOptions.RemoveEmptyEntries);
            if (byte.Parse(data[0]) > 23 || byte.Parse(data[1]) > 59 || byte.Parse(data[2]) > 59) throw new ArgumentOutOfRangeException();
            Hours = byte.Parse(data[0]);
            Minutes = byte.Parse(data[1]);
            Seconds = byte.Parse(data[2]);
        }
        public override string ToString()
        {
            return String.Format("{0:d2}:{1:d2}:{2:d2}", Hours, Minutes, Seconds);
        }
    }
}
