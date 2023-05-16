
namespace TimeLib
{
    /// <summary>
    /// Represents a period of time as seconds.
    /// </summary>
    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        /// <summary>
        /// Gets the <see cref='TimeLib.TimePeriod'/> as seconds.
        /// </summary> 
        public long NumberOfSeconds { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref='TimeLib.TimePeriod'/> with 1, 2 or 3 params specified by <see cref='System.Int64'/> values.
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TimePeriod(long? hours = null, long? minutes = null, long? seconds = null)
        {
            if (hours == null) hours = 0;
            if (minutes == null) minutes = 0;
            if (seconds == null) seconds = 0;
            if (hours < 0 || minutes < 0 || seconds < 0) throw new ArgumentOutOfRangeException("The number of hours, minutes and second cannot be negative.");
            NumberOfSeconds = (long)hours * 3600 + (long)minutes * 60 + (long)seconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref='TimeLib.TimePeriod'/> with 2 params specified by <see cref='TimeLib.Time'/> values. <see cref='TimeLib.TimePeriod'/> is period of time is between two points in time.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public TimePeriod(Time start, Time end)
        {
            if (start == end) NumberOfSeconds = 0;
            else if (start > end)
                NumberOfSeconds = (24 * 3600) - (start.TimeInSeconds() - end.TimeInSeconds());
            else
                NumberOfSeconds = end.TimeInSeconds() - start.TimeInSeconds();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref='TimeLib.TimePeriod'/> with <see cref='System.String'/> param like "hh:mm:ss" or "hh.mm.ss".
        /// </summary>
        /// <param name="s"></param>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        /// <summary>
        /// Converts the <see cref='TimeLib.TimePeriod'/> to its equivalent string representation.
        /// </summary>
        /// <returns>A string that represents this <see cref='TimeLib.Time'/> </returns>
        public override string ToString()
        {
            long hours = NumberOfSeconds / 3600;
            long minutes = (NumberOfSeconds % 3600) / 60;
            long seconds = (NumberOfSeconds % 3600) % 60;
            return String.Format("{0:d2}:{1:d2}:{2:d2}", hours, minutes, seconds);
        }

        /// <summary>
        /// Detemines whether this instance and a specified object, which must also be a <see cref='TimeLib.TimePeriod'/> object, represents the same time.
        /// <see cref='object'/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true if obj is a <see cref='TimeLib.TimePeriod'/> and it represents the same period of time as this instance; otherwise, false. If obj is null, the method returns false.</returns>
        public override bool Equals(object? obj) => obj is TimePeriod time && Equals(time);

        /// <summary>
        /// Detemines whether this instance and another specified Time object, represents the same time.
        /// <see cref='object'/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true if the <paramref name="other"/> parameter represents the same period of time as this instance; otherwise, false. If <paramref name="other"/> is null, the method returns false.</returns>
        public bool Equals(TimePeriod other) => (NumberOfSeconds) == (other.NumberOfSeconds);

        /// <summary>
        /// Returns a hash code for this <see cref='TimeLib.TimePeriod'/>.
        /// </summary>
        /// <returns> An integer value that specifies a hash value for this <see cref='TimeLib.TimePeriod'/>.</returns>
        public override int GetHashCode() => HashCode.Combine(NumberOfSeconds);

        /// <summary>
        /// Compares two <see cref='TimeLib.TimePeriod'/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true, if <paramref name="left"/> and <paramref name="right"/> are equal.</returns>
        public static bool operator ==(TimePeriod left, TimePeriod right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref='TimeLib.TimePeriod'/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true, if <paramref name="left"/> and <paramref name="right"/> are not equal.</returns>
        public static bool operator !=(TimePeriod left, TimePeriod right) => !(left == right);

        /// <summary>
        /// Compares two <see cref='TimeLib.TimePeriod'/> objects.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>An integer indicating the relative values of this instance and value.
        ///<para>Less than zero - This instance is less than value.</para>
        ///<para>Zero - This instance is equal to value.</para>
        ///<para>Greater than zero - This instance is greater than value.</para>
        /// </returns>
        public int CompareTo(TimePeriod other) => (NumberOfSeconds.CompareTo(other.NumberOfSeconds));

        /// <summary>
        /// Compares two <see cref='TimeLib.TimePeriod'/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true, if <paramref name="left"/> is smaller than <paramref name="right"/>; otherwise, false</returns>
        public static bool operator <(TimePeriod left, TimePeriod right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Compares two <see cref='TimeLib.TimePeriod'/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true, if <paramref name="left"/> is smaller or equal to <paramref name="right"/>; otherwise, false</returns>
        public static bool operator <=(TimePeriod left, TimePeriod right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Compares two <see cref='TimeLib.TimePeriod'/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true, if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false</returns>
        public static bool operator >(TimePeriod left, TimePeriod right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Compares two <see cref='TimeLib.TimePeriod'/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true, if <paramref name="left"/> is greater or equal to <paramref name="right"/>; otherwise, false</returns>
        public static bool operator >=(TimePeriod left, TimePeriod right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Performs arithmetic addition <see cref='TimeLib.TimePeriod'/> to <see cref='TimeLib.TimePeriod'/>
        /// </summary>
        /// <param name="t"></param>
        /// <returns><see cref='TimeLib.TimePeriod'/> </returns>
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

        /// <summary>
        /// Performs arithmetic substruction of <see cref='TimeLib.TimePeriod'/> from <see cref='TimeLib.TimePeriod'/>
        /// </summary>
        /// <param name="t"></param>
        /// <returns><see cref='TimeLib.TimePeriod'/> </returns>
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

        /// <summary>
        /// Performs arithmetic addition <see cref='System.Int32'/> to <see cref='TimeLib.TimePeriod'/>
        /// </summary>
        /// <param name="numberOfDays"></param>
        /// <returns><see cref='TimeLib.TimePeriod'/></returns>
        public TimePeriod Plus(int numberOfDays)
        {
            var sumOfSeconds = NumberOfSeconds + numberOfDays * 24 * 3600;
            var h = sumOfSeconds / 3600;
            var m = (sumOfSeconds % 3600) / 60;
            var s = sumOfSeconds % 60;
            return new TimePeriod(h, m, s);
        }
    }
}
