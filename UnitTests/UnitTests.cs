using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using RulesEngine;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        const string generatePackingSlip = "Generate Packing Slip";
            
        [TestMethod]
        public void TestBookRules()
        {
            var owner = Substitute.For<IOwner>();
            owner.Name.Returns("John Doe");
            owner.Email.Returns("jd@mail.com");

            var book = Substitute.For<IBook>();
            book.Owner.Returns(owner);
            book.Name.Returns("Jungle Book");

            var paymentItem = Substitute.For<IPaymentItem>();
            paymentItem.Product.Returns(book);
            paymentItem.Amount.Returns(1.01);

            var payment = Substitute.For<IPayment>();
            payment.Date.Returns(DateTime.Now);
            payment.PaymentItems.Returns(new[] {paymentItem});

            var actionPackingSlip = Substitute.For<IAction>();
            actionPackingSlip.Verb.Returns(generatePackingSlip);
            actionPackingSlip.Arguments.Returns(new[] {"Generic"});

            var actionPackingSlipForBook = Substitute.For<IAction>();
            actionPackingSlipForBook.Verb.Returns(generatePackingSlip);
            actionPackingSlipForBook.Arguments.Returns(new[] {"or Royalty Department"});

            var actions = Substitute.For<IActions>();
            actions.ActionCollection.Returns(new[] {actionPackingSlip, actionPackingSlipForBook});

            var rule = Substitute.For<IRule>();
            rule.Execute().Returns(actions);

            var rules = Substitute.For<IRules>();
            rules.RulesCollection.Returns(new[] {rule});

            var rulesEngine = Substitute.For<RulesEngine.RulesEngine>(rules);
            rulesEngine.Execute(payment).Returns(actions);

            var result = rulesEngine.Execute(payment);

            result.ActionCollection.Count().Should().Be(2);
            result.ActionCollection.Should().ContainItemsAssignableTo<IAction>();
            var resultActions = result.ActionCollection.ToArray();
            resultActions[0].Verb.Should().Be(generatePackingSlip);
        }
    }
}