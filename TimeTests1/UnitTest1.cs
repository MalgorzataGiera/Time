using TimeLib;
namespace TimeTests1
{
    [TestClass]
    public class UnitTest1
    {
        #region Constructor tests =======================================================
        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Time t = new Time();
            Assert.AreEqual((byte)0, t.Hours);
            Assert.AreEqual((byte)0, t.Minutes);
            Assert.AreEqual((byte)0, t.Seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)1, (byte)25, (byte)13)]
        [DataRow((byte)01, (byte)25, (byte)13)]
        public void Constructor_3params(byte h, byte m, byte s)
        {
            Time t = new Time(h, m, s);
            Assert.AreEqual(h, t.Hours);
            Assert.AreEqual(m, t.Minutes);
            Assert.AreEqual(s, t.Seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)13, (byte)7)]
        [DataRow((byte)13, (byte)07)]
        public void Constructor_2params(byte h, byte m)
        {
            Time t = new Time(h, m);
            Assert.AreEqual(h, t.Hours);
            Assert.AreEqual(m, t.Minutes);
            Assert.AreEqual((byte)0, t.Seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)59)]
        [DataRow((byte)09)]
        public void Constructor_1param(byte m)
        {
            Time t = new Time(minutes: m);
            Assert.AreEqual((byte)0, t.Hours);
            Assert.AreEqual(m, t.Minutes);
            Assert.AreEqual((byte)0, t.Seconds);
        }

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_StringParam_ColonMark()
        {
            Time t = new Time("  11:  11 :11    ");
            Assert.AreEqual((byte)11, t.Hours);
            Assert.AreEqual((byte)11, t.Minutes);
            Assert.AreEqual((byte)11, t.Seconds);
        }

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_StringParam_DotMark()
        {
            Time t = new Time("  11. 11.11    ");
            Assert.AreEqual((byte)11, t.Hours);
            Assert.AreEqual((byte)11, t.Minutes);
            Assert.AreEqual((byte)11, t.Seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("12:0:0:09")]
        [DataRow("0:13.9:0")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_StringParam_FormatException(string s)
        {
            Time t = new Time(s);
        }

        // ---------

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)24, (byte)0, (byte)0)]
        [DataRow((byte)0, (byte)60, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)60)]
        [DataRow((byte)24, (byte)60, (byte)0)]
        [DataRow((byte)24, (byte)0, (byte)60)]
        [DataRow((byte)0, (byte)60, (byte)60)]
        [DataRow((byte)24, (byte)60, (byte)60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ArgumentOutOfRangeException(byte h, byte m, byte s)
        {
            Time t = new Time(h, m, s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("24:0:0")]
        [DataRow("0:60:0")]
        [DataRow("0:0:60")]
        [DataRow("24:60:0")]
        [DataRow("24:0:60")]
        [DataRow("0:60:60")]
        [DataRow("24:60:60")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_StringParam_ColonMark_ArgumentOutOfRangeException(string s)
        {
            Time t = new Time(s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("24.0.0")]
        [DataRow("0.60.0")]
        [DataRow("0.0.60")]
        [DataRow("24.60.0")]
        [DataRow("24.0.60")]
        [DataRow("0.60.60")]
        [DataRow("24.60.60")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_StringParam_DotMark_ArgumentOutOfRangeException(string s)
        {
            Time t = new Time(s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("0,0,0")]
        [DataRow("0-0-0")]
        [DataRow("0/0/0")]
        [DataRow("0;0;0")]
        [DataRow("0_0_0")]
        [DataRow("0󪧤")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_StringParam_WrongMark_FormatException(string s)
        {
            Time t = new Time(s);
        }
        #endregion

        #region ToString tests ==========================================================
        [DataTestMethod, TestCategory("String Representation")]
        [DataRow((byte)10, (byte)32, (byte)17, "10:32:17")]
        [DataRow((byte)0, (byte)25, (byte)0, "00:25:00")]
        [DataRow((byte)1, (byte)1, (byte)1, "01:01:01")]
        public void ToStringMethod(byte h, byte m, byte s, string expectedStringRepresentation)
        {
            var t = new Time(h, m, s);
            Assert.AreEqual(expectedStringRepresentation, t.ToString());
        }

        [TestMethod, TestCategory("String Representation")]
        public void Constructor_TimeToStringParam()
        {
            var t = new Time(1, 2, 1);
            var t2 = new Time(t.ToString());
            Assert.AreEqual(t, t2);
        }
        #endregion

        #region Equals ==================================================================
        [TestMethod, TestCategory("Equals")]
        public void IEquatable_Implemented_ReturnTrue()
        {
            Type t = typeof(Time);
            Assert.IsTrue(typeof(IEquatable<Time>).IsAssignableFrom(t));
        }

        [TestMethod, TestCategory("Equals")]
        public void Equals_ComparisonToNullObject_RetunsFalse()
        {
            var t = new Time();
            object obj = null;
            Assert.IsFalse(t.Equals(obj));
        }

        [TestMethod, TestCategory("Equals")]
        public void Equals_ComparisonToTheSameObject_RetunsTrue()
        {
            var t = new Time();
            Assert.IsTrue(t.Equals(t));
        }

        [TestMethod, TestCategory("Equals")]
        public void Equals_ComparisonToDifferentObjects_SameData_ReturnsTrue()
        {
            var t1 = new Time(1, 10, 0);
            var t2 = new Time(1, 10, 0);
            Assert.IsTrue(t1.Equals(t2));
        }

        [TestMethod, TestCategory("Equals")]
        public void Equals_ComparisonToDifferentObjects_DifferentData_ReturnsFalse()
        {
            var t1 = new Time(1, 10, 0);
            var t2 = new Time(11, 25, 30);
            Assert.IsFalse(t1.Equals(t2));
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualSign_ReturnsTrue()
        {
            var t1 = new Time(1, 10, 0);
            Assert.IsTrue(t1 == t1);
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualSign_ReturnsFalse()
        {
            var t1 = new Time(1, 10, 0);
            var t2 = new Time(10, 11, 20);
            Assert.IsFalse(t1 == t2);
        }

        [TestMethod, TestCategory("Equals")]
        public void InequalitySign_ReturnsTrue()

        {
            var t1 = new Time(1, 10, 0);
            var t2 = new Time(10, 11, 20);
            Assert.IsTrue(t1 != t2);
        }
        [TestMethod, TestCategory("Equals")]
        public void InequalitySign_ReturnsFalse()

        {
            var t1 = new Time(1, 10, 0);
            Assert.IsFalse(t1 != t1);
        }
        #endregion

        #region Comparison
        [TestMethod, TestCategory("Comparing")]
        public void IComparable_Implemented_ReturnTrue()
        {
            Type t = typeof(Time);
            Assert.IsTrue(typeof(IComparable<Time>).IsAssignableFrom(t));
        }

        [TestMethod, TestCategory("Comparing")]
        public void Comparison_SmallerThanOtherObject_ReturnsTrue()
        {
            var t1 = new Time(1, 10, 0);
            var t2 = new Time(11, 25, 30);
            Assert.IsTrue(t1.CompareTo(t2) < 0);
            Assert.IsTrue(t1 < t2);
        }

        [TestMethod, TestCategory("Comparing")]
        public void Comparison_BiggerThanOtherObject_ReturnsTrue()
        {
            var t1 = new Time(22, 33, 44);
            var t2 = new Time(11, 25, 30);
            Assert.IsTrue(t1.CompareTo(t2) > 0);
            Assert.IsTrue(t1 > t2);
        }

        [DataTestMethod, TestCategory("Comparing")]
        [DataRow((byte)2, (byte)37, (byte)0, (byte)13, (byte)28, (byte)11)]
        [DataRow((byte)2, (byte)37, (byte)0, (byte)2, (byte)37, (byte)0)]
        public void Comparison_SmallerOrEqualToOtherObject_ReturnsTrue(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            var t1 = new Time(h1, m1, s1);
            var t2 = new Time(h2, m2, s2);
            Assert.IsTrue(t1.CompareTo(t2) <= 0);
            Assert.IsTrue(t1 <= t2);
        }

        [DataTestMethod, TestCategory("Comparing")]
        [DataRow((byte)21, (byte)38, (byte)0, (byte)21, (byte)37, (byte)0)]
        [DataRow((byte)21, (byte)37, (byte)0, (byte)21, (byte)37, (byte)0)]
        public void Comparison_BiggerOrEqualToOtherObject_ReturnsTrue(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            var t1 = new Time(h1, m1, s1);
            var t2 = new Time(h2, m2, s2);
            Assert.IsTrue(t1.CompareTo(t2) >= 0);
            Assert.IsTrue(t1 >= t2);
        }

        [TestMethod, TestCategory("Comparing")]
        public void Comparison_SmallerThanOtherObject_ReturnsFalse()
        {
            var t1 = new Time(11, 25, 30);
            var t2 = new Time(1, 10, 0);
            Assert.IsFalse(t1.CompareTo(t2) < 0);
            Assert.IsFalse(t1 < t2);
        }

        [TestMethod, TestCategory("Comparing")]
        public void Comparison_BiggerThanOtherObject_ReturnsFalse()
        {
            var t1 = new Time(1, 10, 0);
            var t2 = new Time(11, 25, 30);
            Assert.IsFalse(t1.CompareTo(t2) > 0);
            Assert.IsFalse(t1 > t2);
        }

        [TestMethod, TestCategory("Comparing")]
        public void Comparison_SmallerOrEqualToOtherObject_ReturnsFalse()
        {
            var t1 = new Time(11, 25, 30);
            var t2 = new Time(1, 10, 0);
            Assert.IsFalse(t1.CompareTo(t2) <= 0);
            Assert.IsFalse(t1 <= t2);
        }

        [TestMethod, TestCategory("Comparing")]
        public void Comparison_BiggerOrEqualToOtherObject_ReturnsFalse()
        {
            var t1 = new Time(1, 10, 0);
            var t2 = new Time(11, 25, 30);
            Assert.IsFalse(t1.CompareTo(t2) >= 0);
            Assert.IsFalse(t1 >= t2);
        }
        #endregion

        #region arithmetic operations ===================================================
        [DataTestMethod, TestCategory("Addition")]
        [DataRow((byte)0, (byte)0, (byte)0, 1, 1, 1, (byte)1, (byte)1, (byte)1)]
        [DataRow((byte)0, (byte)0, (byte)0, 32, 0, 0, (byte)8, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)59, (byte)59, 0, 0, 1, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0, 0, 0, 0, (byte)0, (byte)0, (byte)0)]
        public void AddingTimePeriod(byte h1, byte m1, byte s1, long h2, long m2, long s2,
            byte h3, byte m3, byte s3)
        {
            var t = new Time(h1, m1, s1);
            var tPeriod = new TimePeriod(h2, m2, s2);
            var expectedNewTime = new Time(h3, m3, s3);
            Assert.AreEqual(expectedNewTime, t.Plus(tPeriod));
        }

        [TestMethod, TestCategory("Addition")]
        public void StaticMethod_AddingTimePeriod()
        {
            var t = new Time(23, 59, 59);
            var tPeriod = new TimePeriod(0, 1, 0);
            var expectedNewTime = new Time(0, 0, 59);
            Assert.AreEqual(expectedNewTime, Time.Plus(t, tPeriod));
        }

        [TestMethod, TestCategory("Addition")]
        public void PlusOperator()
        {
            var t = new Time(23, 59, 59);
            var tPeriod = new TimePeriod(0, 1, 0);
            var expectedNewTime = new Time(0, 0, 59);
            Assert.AreEqual(expectedNewTime, t + tPeriod);
        }

        [DataTestMethod, TestCategory("Subtraction")]
        [DataRow((byte)0, (byte)0, (byte)0, 1, 1, 1, (byte)22, (byte)58, (byte)59)]
        [DataRow((byte)23, (byte)50, (byte)59, 0, 10, 1, (byte)23, (byte)40, (byte)58)]
        [DataRow((byte)0, (byte)0, (byte)0, 0, 0, 0, (byte)0, (byte)0, (byte)0)]
        public void SubstractingTimePeriod(byte h1, byte m1, byte s1, long h2, long m2, long s2,
            byte h3, byte m3, byte s3)
        {
            var t = new Time(h1, m1, s1);
            var tPeriod = new TimePeriod(h2, m2, s2);
            var expectedNewTime = new Time(h3, m3, s3);
            Assert.AreEqual(expectedNewTime, t.Minus(tPeriod));
        }

        [TestMethod, TestCategory("Subtraction")]
        public void StaticMethod_SubstructingTimePeriod()
        {
            var t = new Time(23, 59, 59);
            var tPeriod = new TimePeriod(0, 1, 0);
            var expectedNewTime = new Time(23, 58, 59);
            Assert.AreEqual(expectedNewTime, Time.Minus(t, tPeriod));
        }

        [TestMethod, TestCategory("Subtraction")]
        public void MinusOperator()
        {
            var t = new Time(23, 59, 59);
            var tPeriod = new TimePeriod(0, 1, 0);
            var expectedNewTime = new Time(23, 58, 59);
            Assert.AreEqual(expectedNewTime, t - tPeriod);
        }

        [DataTestMethod, TestCategory("Multiplication")]
        [DataRow((byte)0, (byte)0, (byte)0, 1, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)1, (byte)1, (byte)1, 0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)14, (byte)0, (byte)0, 2, (byte)4, (byte)0, (byte)0)]
        [DataRow((byte)14, (byte)59, (byte)59, 2, (byte)5, (byte)59, (byte)58)]
        [DataRow((byte)12, (byte)0, (byte)0, 2, (byte)0, (byte)0, (byte)0)]
        public void MultiplicationTimePeriodByNumber(byte h1, byte m1, byte s1, int k,
            byte h2, byte m2, byte s2)
        {
            var t = new Time(h1, m1, s1);
            var expectedTime = new Time(h2, m2, s2);
            Assert.AreEqual(expectedTime, t * k);
        }

        [TestMethod, TestCategory("Multiplication")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MultiplicationTimePeriodByWrongNumber()
        {
            var t = new Time(1, 1, 1);
            var t2 = t * -1;
        }
        #endregion
    }
}