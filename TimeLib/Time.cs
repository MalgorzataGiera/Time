

namespace TimeLib
{
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
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
        
        public Time(string s) // for string in format hh:mm:ss or hh.mm.ss
        {
            char[] chars = { ':', '.' };
            string[] data = s.Split(chars, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length > 3) throw new FormatException("You cannot enter more than 3 parameters");
            if (byte.Parse(data[0]) > 23 || byte.Parse(data[1]) > 59 || byte.Parse(data[2]) > 59) throw new ArgumentOutOfRangeException();
            Hours = byte.Parse(data[0]);
            Minutes = byte.Parse(data[1]);
            Seconds = byte.Parse(data[2]);
        }
        public override string ToString()
        {
            return String.Format("{0:d2}:{1:d2}:{2:d2}", Hours, Minutes, Seconds);
        }

        public override bool Equals(object? obj) => obj is Time time && Equals(time);

        public bool Equals(Time other) => (Hours, Minutes, Seconds) == (other.Hours, other.Minutes, other.Seconds);

        public override int GetHashCode()
        {
            return HashCode.Combine(Hours, Minutes, Seconds);
        }

        public static bool operator ==(Time left, Time right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Time left, Time right)
        {
            return !(left == right);
        }

        public int CompareTo(Time other)
        {
            return (this.TimeInSeconds()).CompareTo(other.TimeInSeconds());
        }

        public static bool operator <(Time left, Time right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Time left, Time right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Time left, Time right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Time left, Time right)
        {
            return left.CompareTo(right) >= 0;
        }

        public int TimeInSeconds()
        {
            return this.Seconds + this.Minutes * 60 + this.Hours * 3600;
        }

        public Time Plus(TimePeriod t)
        {
            var sumOfSeconds = TimeInSeconds() + t.NumberOfSeconds;
            var h = (sumOfSeconds / 3600) % 24;
            var m = ((sumOfSeconds % 3600) / 60) % 60;
            var s = ((sumOfSeconds % 3600) % 60);
            return new Time((byte)h, (byte)m, (byte)s);
        }

        public static Time Plus(Time t, TimePeriod tPeriod) => t.Plus(tPeriod);

        public static Time operator +(Time t, TimePeriod tPeriod) => t.Plus(tPeriod);

        public Time Minus(TimePeriod t)
        {
            if (t.NumberOfSeconds == 0) return this;
            var sumOfSeconds = TimeInSeconds() - t.NumberOfSeconds;
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
            return new Time((byte)h, (byte)m, (byte)s);
        }

        public static Time Minus(Time t, TimePeriod tPeriod) => t.Minus(tPeriod);

        public static Time operator -(Time t, TimePeriod tPeriod) => t.Minus(tPeriod);

        public static Time operator *(Time t, int k)
        {
            if (k < 0) throw new ArgumentOutOfRangeException("You cannot multiply type Time by a negative number");
            var newTimeInSeconds = t.TimeInSeconds() * k;
            var h = (newTimeInSeconds / 3600) % 24;
            var m = ((newTimeInSeconds % 3600) / 60) % 60;
            var s = ((newTimeInSeconds % 3600) % 60);
            return new Time((byte)h, (byte)m, (byte)s);
        }
    }
}
