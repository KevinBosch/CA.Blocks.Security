using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

namespace CA.Blocks.SecurityTests.Cryptography
{
    [TestFixture]
    public class MD5_UnitTests
    {

        #region Test_generateMd5Hash
        [Test]
        public void Test_generateMd5Hash()
        {
            // get a MD5 hash from a string..
            string md5hashUsingtest = Security.Cryptography.MD5.GenerateHashAsBase64String("test");
            // set up a pre canned result which is known
            string expectedHashResult = "CY9rzUYh03PK3k6DJie09g==";
            Assert.AreEqual(expectedHashResult, md5hashUsingtest);

            // now generate a hash for same string with a single capital letter in it. 
            string md5hashUsingTest = CA.Blocks.Security.Cryptography.MD5.GenerateHashAsBase64String("Test");
            // we expect the hash result should be diffrent.
            Assert.AreNotEqual(md5hashUsingTest, md5hashUsingtest);
        }


        [Test]
        public void Test_generateMd5HashAsHexadecimalString()
        {
            // get a MD5 hash from a string..
            string md5hashUsingtest = CA.Blocks.Security.Cryptography.MD5.GenerateHashAsHexadecimalString("test");
            // set up a pre canned result which is known
            //Trace.WriteLine(md5hashUsingtest);
            string expectedHashResult = "098f6bcd4621d373cade4e832627b4f6";
            Assert.AreEqual(expectedHashResult, md5hashUsingtest);

            // now generate a hash for same string with a single capital letter in it. 
            string md5hashUsingTest = CA.Blocks.Security.Cryptography.MD5.GenerateHashAsHexadecimalString("Test");
            // we expect the hash result should be diffrent.
            Assert.AreNotEqual(md5hashUsingTest, md5hashUsingtest);
        }


        #endregion

        #region Test_GenerateMd5CaseInsensitiveHash
        [Test]
        public void Test_GenerateMd5CaseInsensitiveHash()
        {
            // get a CaseInsensitive MD5 hash from a string..
            string md5hashUsingtest = Security.Cryptography.MD5.GenerateCaseInsensitiveHashAsBase64String("test");
            // set up a pre canned result which is known
            string expectedHashResult = "CY9rzUYh03PK3k6DJie09g==";
            Assert.AreEqual(expectedHashResult, md5hashUsingtest);

            // now generate a hash for same string with a single capital letter in it.
            string md5hashUsingTest = Security.Cryptography.MD5.GenerateCaseInsensitiveHashAsBase64String("Test");
            // we expect the hash result should be the same. as the hash should be CaseInsensitive
            Assert.AreEqual(md5hashUsingTest, md5hashUsingtest);
        }
        #endregion


         #region Test_GenerateMd5CaseInsensitiveHash
        [Test]
        public void Test_GenerateCaseInsensitiveHashAsHexadecimalString()
        {
            string newPass = "J8RTSTM4";
            string md5hashUsingtest = Security.Cryptography.MD5.GenerateCaseInsensitiveHashAsHexadecimalString(newPass);
            Trace.WriteLine(md5hashUsingtest);

            string md5hashUsingtest1 = Security.Cryptography.MD5.GenerateHashAsHexadecimalString(newPass);
            Trace.WriteLine(md5hashUsingtest1);

        }


        #endregion
    }
}
