using System;
using IndyDotNet.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.InternalTests
{
    [TestClass]
    public class EncodingTests
    {
        [TestMethod]
        public void EncodeStringAsSha256DecimalSuccessfully()
        {
            string state = "UT";
            string encoded = state.AsSha256Decimal();

            Assert.AreEqual("93856629670657830351991220989031130499313559332549427637940645777813964461231", encoded, $"AsSha256Decimal() encoded returned {encoded}");
        }

        [TestMethod]
        public void EncodeNumberStringAsSha256DecimalCorrectly()
        {
            string zip = "84020";
            string encoded = zip.AsSha256Decimal();

            Assert.AreEqual("84020", encoded, $"AsSha256Decimal() encoded returned {encoded}");
        }

    }
}
