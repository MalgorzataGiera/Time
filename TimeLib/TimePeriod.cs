
namespace TimeLib
{
    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
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
            if (data.Length != 3) throw new FormatException("You cannot enter more than 3 parameters");
            if (long.Parse(data[0]) < 0 || long.Parse(data[1]) < 0 || long.Parse(data[2]) < 0) throw new ArgumentOutOfRangeException();
            var hourIsNumber = Int64.TryParse(data[0], out long hours);
            var minuteIsNumber = Int64.TryParse(data[1], out long minutes);
            var secondIsNumber = Int64.TryParse(data[2], out long seconds);
            if (!hourIsNumber || !minuteIsNumber || !secondIsNumber) throw new FormatException("All parameters must be numbers");
            NumberOfSeconds = hours * 3600 + minutes * 60 + seconds;
        }

        public override string ToString()
        {
            long hours = NumberOfSeconds / 3600;
            long minutes = (NumberOfSeconds % 3600) / 60;
            long seconds = (NumberOfSeconds % 3600) % 60;
            return String.Format("{0:d2}:{1:d2}:{2:d2}", hours, minutes, seconds);
        }

        public override bool Equals(object? obj) => obj is TimePeriod time && Equals(time);

        public bool Equals(TimePeriod other) => (NumberOfSeconds) == (other.NumberOfSeconds);

        public override int GetHashCode() => HashCode.Combine(NumberOfSeconds);
        
        public static bool operator ==(TimePeriod left, TimePeriod right) => left.Equals(right);
        
        public static bool operator !=(TimePeriod left, TimePeriod right) => !(left == right);
        
        public int CompareTo(TimePeriod other) => (NumberOfSeconds.CompareTo(other.NumberOfSeconds));
        
        public static bool operator <(TimePeriod left, TimePeriod right) => left.CompareTo(right) < 0;
        
        public static bool operator <=(TimePeriod left, TimePeriod right) => left.CompareTo(right) <= 0;
        
        public static bool operator >(TimePeriod left, TimePeriod right) => left.CompareTo(right) > 0;
        
        public static bool operator >=(TimePeriod left, TimePeriod right) => left.CompareTo(right) >= 0;
        
        public TimePeriod Plus (TimePeriod tP1)
        {
            var sumOfSeconds = NumberOfSeconds + tP1.NumberOfSeconds;
            var h = sumOfSeconds / 3600;
            var m = (sumOfSeconds % 3600) / 60;
            var s = sumOfSeconds % 60;
            return new TimePeriod(h, m, s);
        }

        public static TimePeriod Plus(TimePeriod tP1, TimePeriod tp2) => tP1.Plus(tp2);

        public static TimePeriod operator +(TimePeriod left, TimePeriod right) => left.Plus(right);

        public TimePeriod Minus(TimePeriod t)
        {
            if (t.NumberOfSeconds == 0) return this;
            var sumOfSeconds = NumberOfSeconds - t.NumberOfSeconds;
            var h = (sumOfSeconds / 3600) % 24;
            if (h <= 0) h = (24 + h) % 24;
            var m = ((sumOfSeconds % 3600) / 60) % 60;
            if (m <= 0)
            {
                m = 60 + m;
                if (h == 0) h = 23;
                else h = h - 1;
            }
            var s = ((sumOfSeconds % 3600) % 60);
            if (s <= 0)
            {
                s = 60 + s;
                if (m == 0) m = 59;
                else m = m - 1;
            }
            return new TimePeriod(h, m, s);
        }

        public static TimePeriod Minus(TimePeriod t, TimePeriod tPeriod) => t.Minus(tPeriod);

        public static TimePeriod operator -(TimePeriod t, TimePeriod tPeriod) => t.Minus(tPeriod);

        public static TimePeriod operator *(TimePeriod t, int k)
        {
            if (k < 0) throw new ArgumentOutOfRangeException("You cannot multiply type Time by a negative number");
            var newTimeInSeconds = t.NumberOfSeconds * k;
            var h = newTimeInSeconds / 3600;
            var m = (newTimeInSeconds % 3600) / 60;
            var s = newTimeInSeconds % 60;
            return new TimePeriod(h, m, s);
        }
    }
}
