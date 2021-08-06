using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using RulesEngine;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var owner = new Owner("John Doe", "jd@mail.com");
            var product = new Book("Jungle Book", owner);
            var paymentItem = new PaymentItem(product, 1.01);
            var payment = new Payment(DateTime.Now, new []{paymentItem});
            var rulesEngine = new RulesEngine.RulesEngine();
            rulesEngine.Execute(payment);
        }
    }
}
