using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestQuipu.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TestQuipu.Logic.Tests
{
    [TestClass()]
    public class HtmlLoaderTests
    {
        [TestMethod()]
        public void LoadHtmlCodeTest()
        {
            HtmlLoader loader = new HtmlLoader();
            var res = loader.LoadHtmlCode("http://www.opera.com/ru");

            Assert.IsTrue(Regex.Matches(res, "<html").Count > 0);
            //Assert.Fail();
        }
        [TestMethod()]
        public void LoadHtmlCodeTestNoHttp()
        {
            HtmlLoader loader = new HtmlLoader();
            var res = loader.LoadHtmlCode("www.opera.com/ru");

            Assert.IsTrue(Regex.Matches(res, "<html").Count > 0);
            //Assert.Fail();
        }
        [TestMethod()]
        public void LoadHtmlCodeTestnoWWW()
        {
            HtmlLoader loader = new HtmlLoader();
            var res = loader.LoadHtmlCode("opera.com/ru");

            Assert.IsTrue(Regex.Matches(res, "<html").Count > 0);
            //Assert.Fail();
        }
    }

}