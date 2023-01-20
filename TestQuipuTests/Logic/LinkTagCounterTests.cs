using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestQuipu.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestQuipu.Logic.Tests
{
    [TestClass()]
    public class LinkTagCounterTests
    {
        [TestMethod()]
        public void countLinkTagsTest()
        {
            string html = "<html><body><a></a><a href ='sadsad'></a><a>dasdasda</a></body></html>";
            int expect = 3;

            LinkTagCounter counter = new LinkTagCounter();
            int res = counter.CountLinkTags(html);


            Assert.AreEqual(expect, res);
        }
        [TestMethod()]
        public void countLinkTagsTestWithSpaces()
        {
            string html = "<html><body>< a></a><  a href ='sadsad'></a><   a>dasdasda</a></body></html>";
            int expect = 3;

            LinkTagCounter counter = new LinkTagCounter();
            int res = counter.CountLinkTags(html);


            Assert.AreEqual(expect, res);
        }
    }
}