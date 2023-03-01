using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Token.YonYou;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Token.YonYou.Tests
{
    [TestClass()]
    public class TokenHelperTests
    {
        [TestMethod()]
        public void GetSelfAccessTokenTest()
        {
            string filePath = @"E:\_GitHub\ML.Token\ML.TokenTests\config.json";
            string ysEnv = "yonsuite-sandbox";
            string result = TokenHelper.GetSelfAccessToken(filePath,ysEnv);
            Console.WriteLine(result);
            //Assert.Fail();
        }
    }
}