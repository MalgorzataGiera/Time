
namespace TimeLib
{
    /// <summary>
    /// Represents a point in time between 00:00:00 … 23:59:59.
    /// </summary>
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        /// <summary>
        /// Gets the string <see cref='TimeLib.Time'/> component: Hours.
        /// </summary> 
        public byte Hours { get; }

        /// <summary>
        /// Gets the <see cref='TimeLib.Time'/> component: Minutes.
        /// </summary>
        public byte Minutes { get; }

        /// <summary>
        /// Gets the <see cref='TimeLib.Time'/> component: Seconds.
        /// </summary>
        public byte Seconds { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref='TimeLib.Time'/> with 1, 2 or 3 params specified by <see cref='System.Byte'/> values.
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Time(byte? hours = null, byte? minutes = null, byte? seconds = null)
        {
            if (hours == null) hours = 0;
            if (minutes == null) minutes = 0;
            if (seconds == null) seconds = 0;
            if (hours > 23) throw new ArgumentOutOfRangeException("The number of hours cannot be greater than 23.");
            if (minutes > 59 || seconds > 59) throw new ArgumentOutOfRangeException("The number of minutes or seconds cannot be greater than 59.");

            Hours = (byte)hours;
            Minutes = (byte)minutes;
            Seconds = (byte)seconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref='TimeLib.Time'/> with <see cref='System.String'/> param like "hh:mm:ss" or "hh.mm.ss".
        /// </summary>
        /// <param name="s"></param>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Time(string s) // for string in format hh:mm:ss or hh.mm.ss
        {
            char[] chars = { ':', '.' };
            string[] data = s.Split(chars, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length != 3) throw new FormatException("You cannot enter more than 3 parameters");
            if (byte.Parse(data[0]) > 23 || byte.Parse(data[1]) > 59 || byte.Parse(data[2]) > 59) throw new ArgumentOutOfRangeException();
            var HourIsNumber = Byte.TryParse(data[0], out byte hours);
            var MinuteIsNumber = Byte.TryParse(data[1], out byte minutes); 
            var SecondIsNumber = Byte.TryParse(data[2], out byte seconds);
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        /// <summary>
        /// Converts the <see cref='TimeLib.Time'/> to its equivalent string representation.
        /// </summary>
        /// <returns>A string that represents this <see cref='TimeLib.Time'/> </returns>
        public override string ToString() => String.Format("{0:d2}:{1:d2}:{2:d2}", Hours, Minutes, Seconds);

        /// <summary>
        /// Detemines whether this instance and a specified object, which must also be a <see cref='TimeLib.Time'/> object, represents the same time.
        /// <see cref='object'/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true if obj is a string and it represents the same time as this instance; otherwise, false. If obj is null, the method returns false.</returns>
        public override bool Equals(object? obj) => obj is Time time && Equals(time);

        /// <summary>
        /// Detemines whether this instance and another specified Time object, represents the same time.
        /// <see cref='object'/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true if the <paramref name="other"/> parameter represents the same time as this instance; otherwise, false. If <paramref name="other"/> is null, the method returns false.</returns>
        public bool Equals(Time other) => (Hours, Minutes, Seconds) == (other.Hours, other.Minutes, other.Seconds);

        /// <summary>
        /// Returns a hash code for this <see cref='TimeLib.Time'/>.
        /// </summary>
        /// <returns> An integer value that specifies a hash value for this <see cref='TimeLib.Time'/>.</returns>
        public override int GetHashCode() => HashCode.Combine(Hours, Minutes, Seconds);

        /// <summary>
        /// Compares two <see cref='TimeLib.Time'/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true, if <paramref name="left"/> and <paramref name="right"/> are equal.</returns>
        public static bool operator ==(Time left, Time right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref='TimeLib.Time'/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true, if <paramref name="left"/> and <paramref name="right"/> are not equal.</returns>
        public static bool operator !=(Time left, Time right) => !(left == right);

        /// <summary>
        /// Compares two <see cref='TimeLib.Time'/> objects.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>An integer indicating the relative values of this instance and value.
        ///<para>Less than zero - This instance is less than value.</para>
        ///<para>Zero - This instance is equal to value.</para>
        ///<para>Greater than zero - This instance is greater than value.</para>
        /// </returns>
        public int CompareTo(Time other) => TimeInSeconds().CompareTo(other.TimeInSeconds());

        /// <summary>
        /// Compares two <see cref='TimeLib.Time'/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true, if <paramref name="left"/> is smaller than <paramref name="right"/>; otherwise, false</returns>
        public static bool operator <(Time left, Time right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Compares two <see cref='TimeLib.Time'/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true, if <paramref name="left"/> is smaller or equal to <paramref name="right"/>; otherwise, false</returns>
        public static bool operator <=(Time left, Time right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Compares two <see cref='TimeLib.Time'/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true, if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false</returns>
        public static bool operator >(Time left, Time right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Compares two <see cref='TimeLib.Time'/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true, if <paramref name="left"/> is greater or equal to <paramref name="right"/>; otherwise, false</returns>
        public static bool operator >=(Time left, Time right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Converts <see cref='TimeLib.Time'/> to seconds.
        /// </summary>
        /// <returns>time in seconds as <see cref='System.Int32'/></returns>
        public int TimeInSeconds() => Seconds + Minutes * 60 + Hours * 3600;

        /// <summary>
        /// Performs arithmetic addition <see cref='TimeLib.TimePeriod'/> to <see cref='TimeLib.Time'/>
        /// </summary>
        /// <param name="t"></param>
        /// <returns><see cref='TimeLib.Time'/> </returns>
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

        /// <summary>
        /// Performs arithmetic substruction of <see cref='TimeLib.TimePeriod'/> from <see cref='TimeLib.Time'/>
        /// </summary>
        /// <param name="t"></param>
        /// <returns><see cref='TimeLib.Time'/> </returns>
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
