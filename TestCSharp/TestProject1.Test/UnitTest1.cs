using NUnit.Framework;

namespace TestProject1.Test
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Assert.AreEqual((0, 3), TestCSharp.P003.PrimeCount(3, 2));
            Assert.AreEqual((1, 3), TestCSharp.P003.PrimeCount(6, 2));
            Assert.AreEqual((2, 3), TestCSharp.P003.PrimeCount(12, 2));
        }
    }
}
