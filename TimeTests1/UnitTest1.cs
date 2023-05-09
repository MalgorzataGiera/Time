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

        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)1, (byte)25, (byte)13)]
        [DataRow((byte)01, (byte)25, (byte)13)]
        public void Constructor_3params(byte h, byte m, byte s)
        {
            Time t = new Time(h, m, s);
            Assert.AreEqual(h, t.Hours);
            Assert.AreEqual(m, t.Minutes);
            Assert.AreEqual(s, t.Seconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)13, (byte)7)]
        [DataRow((byte)13, (byte)07)]
        public void Constructor_2params(byte h, byte m)
        {
            Time t = new Time(h, m);
            Assert.AreEqual(h, t.Hours);
            Assert.AreEqual(m, t.Minutes);
            Assert.AreEqual((byte)0, t.Seconds);
        }

        [TestMethod, TestCategory("Constructors")]
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
        [DataRow("  11:  11 :11    ")]
        public void Constructor_StringParam_ColonMark(string s)
        {

            Time t = new Time(s);
            Assert.AreEqual((byte)11, t.Hours);
            Assert.AreEqual((byte)11, t.Minutes);
            Assert.AreEqual((byte)11, t.Seconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("  11. 11.11    ")]
        public void Constructor_StringParam_DotMark(string s)
        {

            Time t = new Time(s);
            Assert.AreEqual((byte)11, t.Hours);
            Assert.AreEqual((byte)11, t.Minutes);
            Assert.AreEqual((byte)11, t.Seconds);
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
    }
}