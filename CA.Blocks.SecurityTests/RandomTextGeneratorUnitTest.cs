using System.Diagnostics;
using CA.Blocks.Security;
using NUnit.Framework;

namespace CA.Blocks.SecurityTests
{
    [TestFixture]
    public class RandomTextGeneratorUnitTest
    {

        [Test]
        public void GenerateTest()
        {
            RandomTextGenerator gen = new RandomTextGenerator();
            string test = gen.Generate();
            Trace.WriteLine(test);
            Assert.AreEqual(test.Length, gen.Settings.Length);
        }

        [Test]
        public void GenerateLengthTest()
        {
            RandomTextGenerator gen = new RandomTextGenerator(20);
            string test = gen.Generate();
            Trace.WriteLine(test);
            Assert.AreEqual(test.Length, gen.Settings.Length);
        }

        [Test]
        public void GenerateLargeTest()
        {
            RandomTextGenerator.RandomTextGeneratorSettings gensettings =
                new RandomTextGenerator.RandomTextGeneratorSettings
                {
                    Length = 1000,
                    AllowedChars = "ABCDEFGEHIJKLNMOPQRSTUVXYZ012345789"
                };
            RandomTextGenerator gen = new RandomTextGenerator();
            string test = gen.Generate();
            Trace.WriteLine(test);
            Assert.AreEqual(test.Length, gen.Settings.Length);
        }
    }
}
