using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace EncurtadorURL.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int tamanho = 5;
            string caracteres = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();

            var result = new string(Enumerable.Repeat(caracteres, tamanho).Select(s => s[random.Next(s.Length)]).ToArray());

            Assert.AreNotEqual(result, string.Empty);

        }
    }
}
