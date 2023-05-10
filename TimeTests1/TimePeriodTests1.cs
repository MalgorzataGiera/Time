using TimeLib;
namespace TimeTests1
{
    [TestClass]
    public class UnitTests_TimePeriod
    {
        #region Constructor tests =======================================================
        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            TimePeriod t = new TimePeriod();
            Assert.AreEqual(0, t.NumberOfSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(1, 25, 31, 5131)]
        [DataRow(0, 15, 02, 902)]
        [DataRow(32, 15, 00, 116100)]
        public void Constructor_3params(long h, long m, long s, long expectedNumOfSec)
        {
            TimePeriod t = new TimePeriod(h, m, s);
            Assert.AreEqual(expectedNumOfSec, t.NumberOfSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(2, 23, 8580)]
        [DataRow(10, 15, 36900)]
        [DataRow(32, 64, 119040)]
        public void Constructor_2params(long h, long m, long expectedNumOfSec)
        {
            TimePeriod t = new TimePeriod(h, m);
            Assert.AreEqual(expectedNumOfSec, t.NumberOfSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(7, 420)]
        [DataRow(123, 7380)]
        public void Constructor_1param(long m, long expectedNumOfSec)
        {
            TimePeriod t = new TimePeriod(minutes: m);
            Assert.AreEqual(expectedNumOfSec, t.NumberOfSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("  11:  11 :11    ", 40271)]
        [DataRow("  115:  51 :00    ", 417060)]
        [DataRow("  10.  81 :17    ", 40877)]
        [DataRow("  135.  56 .00    ", 489360)]
        public void Constructor_StringParam(string s, long expectedNumOfSec)
        {

            TimePeriod t = new TimePeriod(s);
            Assert.AreEqual(expectedNumOfSec, t.NumberOfSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)7, (byte)20, (byte)9, (byte)20, 7200)]
        [DataRow((byte)12, (byte)23, (byte)13, (byte)30, 4020)]
        [DataRow((byte)1, (byte)00, (byte)00, (byte)00, 82800)]
        [DataRow((byte)00, (byte)00, (byte)00, (byte)00, 0)]
        public void Constructor_TimeParams(byte h1, byte m1, byte h2, byte m2, long expectedNumOfSec)
        {
            Time start = new Time(h1, m1);
            Time end = new Time(h2, m2);
            TimePeriod t = new TimePeriod(start, end);
            Assert.AreEqual(expectedNumOfSec, t.NumberOfSeconds);
        }

        // ---------

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 0, 0)]
        [DataRow(0, -1, 0)]
        [DataRow(0, 0, -1)]
        [DataRow(-1, -1, 0)]
        [DataRow(-1, 0, -1)]
        [DataRow(0, -1, -1)]
        [DataRow(-1, -1, -1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ArgumentOutOfRangeException(long h, long m, long s)
        {
            TimePeriod t = new TimePeriod(h, m, s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("-1:0:0")]
        [DataRow("0:-1:0")]
        [DataRow("0:0:-1")]
        [DataRow("-1.-1.0")]
        [DataRow("-1.0.-1")]
        [DataRow("0.-1.-1")]
        [DataRow("-1.-1.-1")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_StringParam_ArgumentOutOfRangeException(string s)
        {
            TimePeriod t = new TimePeriod(s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("12:0:0:09")]
        [DataRow("0:13.9:0")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_StringParam_FormatException(string s)
        {
            TimePeriod t = new TimePeriod(s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("0,0,0")]
        [DataRow("0-0-0")]
        [DataRow("0/0/0")]
        [DataRow("0;0;0")]
        [DataRow("0_0_0")]
        [DataRow("0×0×0")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_StringParam_WrongMark_FormatException(string s)
        {
            TimePeriod t = new TimePeriod(s);
        }
        #endregion

        #region ToString tests ==========================================================
        [DataTestMethod, TestCategory("String Representation")]
        [DataRow(10, 32, 17, "10:32:17")]
        [DataRow(129, 58, 12, "129:58:12")]
        [DataRow(29, 58, 00, "29:58:00")]
        public void ToStringMethod(long h, long m, long s, string expectedStringRepresentation)
        {
            var t = new TimePeriod(h, m, s);
            Assert.AreEqual(expectedStringRepresentation, t.ToString());
        }
        #endregion

        #region Equals ==================================================================
        [TestMethod, TestCategory("Equals")]
        public void IEquatable_Implemented_ReturnTrue()
        {
            Type t = typeof(TimePeriod);
            Assert.IsTrue(typeof(IEquatable<TimePeriod>).IsAssignableFrom(t));
        }

        [TestMethod, TestCategory("Equals")]
        public void Equals_ComparisonToTheSameObject_RetunsTrue()
        {
            var t = new TimePeriod();
            Assert.IsTrue(t.Equals(t));
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow(1, 10, 0, 1, 10, 0)]
        [DataRow(2, 0, 0, 0, 120, 0)]
        [DataRow(0, 10, 0, 0, 0, 600)]
        public void Equals_ComparisonToDifferentObjects_SameData_ReturnsTrue(long h1, long m1, long s1, long h2, long m2, long s2)
        {
            var t1 = new TimePeriod(h1, m1, s1);
            var t2 = new TimePeriod(h2, m2, s2);
            Assert.IsTrue(t1.Equals(t2));
        }

        [TestMethod, TestCategory("Equals")]
        public void Equals_ComparisonToDifferentObjects_DifferentData_ReturnsFalse()
        {
            var t1 = new TimePeriod(1, 10, 0);
            var t2 = new TimePeriod(11, 25, 30);
            Assert.IsFalse(t1.Equals(t2));
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualSign_ReturnsTrue()
        {
            var t1 = new TimePeriod(1, 10, 0);
            var t2 = new TimePeriod(1, 10, 0);
            Assert.IsTrue(t1 == t2);
        }

        [TestMethod, TestCategory("Equals")]
        public void EqualSign_ReturnsFalse()
        {
            var t1 = new TimePeriod(1, 10, 0);
            var t2 = new TimePeriod(10, 11, 20);
            Assert.IsFalse(t1 == t2);
        }

        [TestMethod, TestCategory("Equals")]
        public void InequalitySign_ReturnsTrue()

        {
            var t1 = new TimePeriod(1, 10, 0);
            var t2 = new TimePeriod(10, 11, 20);
            Assert.IsTrue(t1 != t2);
        }
        [TestMethod, TestCategory("Equals")]
        public void InequalitySign_ReturnsFalse()

        {
            var t1 = new TimePeriod(1, 10, 0);
            var t2 = new TimePeriod(0, 65, 300);
            Assert.IsFalse(t1 != t2);
        }
        #endregion

        #region Comparison
        [TestMethod, TestCategory("Comparing")]
        public void IComparable_Implemented_ReturnTrue()
        {
            Type t = typeof(TimePeriod);
            Assert.IsTrue(typeof(IComparable<TimePeriod>).IsAssignableFrom(t));
        }

        [TestMethod, TestCategory("Comparing")]
        public void Comparison_SmallerThanOtherObject_ReturnsTrue()
        {
            var t1 = new TimePeriod(1, 10, 0);
            var t2 = new TimePeriod(11, 25, 30);
            Assert.IsTrue(t1.CompareTo(t2) < 0);
            Assert.IsTrue(t1 < t2);
        }

        [TestMethod, TestCategory("Comparing")]
        public void Comparison_BiggerThanOtherObject_ReturnsTrue()
        {
            var t1 = new TimePeriod(22, 33, 44);
            var t2 = new TimePeriod(11, 25, 30);
            Assert.IsTrue(t1.CompareTo(t2) > 0);
            Assert.IsTrue(t1 > t2);
        }

        [TestMethod, TestCategory("Comparing")]
        [DataRow(12, 00, 00, 15, 15, 22)]
        [DataRow(0, 2, 1, 0, 0, 240)]
        [DataRow(1, 2, 10, 0, 62, 10)]
        public void Comparison_SmallerOrEqualToOtherObject_ReturnsTrue(long h1, long m1, long s1, long h2, long m2, long s2)
        {
            var t1 = new TimePeriod(h1, m1, s1);
            var t2 = new TimePeriod(h2, m2, s2);
            Assert.IsTrue(t1.CompareTo(t2) <= 0);
            Assert.IsTrue(t1 <= t2);
        }

        [TestMethod, TestCategory("Comparing")]
        [DataRow(12, 00, 00, 00, 00, 43200)]
        [DataRow(0, 2, 1, 0, 2, 0)]
        [DataRow(15, 21, 10, 0, 63, 10)]
        public void Comparison_BiggerOrEqualToOtherObject_ReturnsTrue(long h1, long m1, long s1, long h2, long m2, long s2)
        {
            var t1 = new TimePeriod(h1, m1, s1);
            var t2 = new TimePeriod(h2, m2, s2);
            Assert.IsTrue(t1.CompareTo(t2) >= 0);
            Assert.IsTrue(t1 >= t2);
        }

        [TestMethod, TestCategory("Comparing")]
        public void Comparison_SmallerThanOtherObject_ReturnsFalse()
        {
            var t1 = new TimePeriod(11, 25, 30);
            var t2 = new TimePeriod(1, 10, 0);
            Assert.IsFalse(t1.CompareTo(t2) < 0);
            Assert.IsFalse(t1 < t2);
        }

        [TestMethod, TestCategory("Comparing")]
        public void Comparison_BiggerThanOtherObject_ReturnsFalse()
        {
            var t1 = new TimePeriod(1, 10, 0);
            var t2 = new TimePeriod(11, 25, 30);
            Assert.IsFalse(t1.CompareTo(t2) > 0);
            Assert.IsFalse(t1 > t2);
        }

        [TestMethod, TestCategory("Comparing")]
        public void Comparison_SmallerOrEqualToOtherObject_ReturnsFalse()
        {
            var t1 = new TimePeriod(11, 25, 30);
            var t2 = new TimePeriod(1, 10, 0);
            Assert.IsFalse(t1.CompareTo(t2) <= 0);
            Assert.IsFalse(t1 <= t2);
        }

        [TestMethod, TestCategory("Comparing")]
        public void Comparison_BiggerOrEqualToOtherObject_ReturnsFalse()
        {
            var t1 = new TimePeriod(1, 10, 0);
            var t2 = new TimePeriod(11, 25, 30);
            Assert.IsFalse(t1.CompareTo(t2) >= 0);
            Assert.IsFalse(t1 >= t2);
        }
        #endregion
    }
}
