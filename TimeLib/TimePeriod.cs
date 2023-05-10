
namespace TimeLib
{
    public readonly struct TimePeriod
    {
        public long NumberOfSeconds { get; }
        public TimePeriod(long? hours = null, long? minutes = null, long? seconds = null)
        {
            if (hours == null) hours = 0;
            if (minutes == null) minutes = 0;
            if (seconds == null) seconds = 0;
            if (hours < 0 || minutes < 0 || seconds < 0) throw new ArgumentOutOfRangeException();
            NumberOfSeconds = (long)hours * 3600 + (long)minutes * 60 + (long)seconds;
        }
        public TimePeriod(Time start, Time end)
        {
            if (start == end) NumberOfSeconds = 0;
            else if (start > end)
                NumberOfSeconds = (24 * 3600) - (start.TimeInSeconds() - end.TimeInSeconds());
            else
                NumberOfSeconds = end.TimeInSeconds() - start.TimeInSeconds();
        }
        public TimePeriod(string s) // for string in format hh:mm:ss or hh.mm.ss
        {
            char[] chars = { ':', '.' };
            string[] data = s.Split(chars, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length > 3) throw new FormatException("You cannot enter more than 3 parameters");
            if (long.Parse(data[0]) < 0 || long.Parse(data[1]) < 0 || long.Parse(data[2]) < 0) throw new ArgumentOutOfRangeException();
            var hours = long.Parse(data[0]);
            var minutes = long.Parse(data[1]);
            var seconds = long.Parse(data[2]);
            NumberOfSeconds = hours * 3600 + minutes * 60 + seconds;
        }
    }
}