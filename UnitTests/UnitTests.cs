﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        const string GeneratePackingSlip = "Generate Packing Slip";
        const string Shipping = "Shipping";
        const string ForRoyaltyDepartment = "For Royalty Department";
        const string MembershipActivation = "Membership Activation";
        const string Activate = "Activate";

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
            actionPackingSlip.Verb.Returns(GeneratePackingSlip);
            actionPackingSlip.Arguments.Returns(new[] { Shipping });

            var actionPackingSlipForBook = Substitute.For<IAction>();
            actionPackingSlipForBook.Verb.Returns(GeneratePackingSlip);
            actionPackingSlipForBook.Arguments.Returns(new[] { ForRoyaltyDepartment });

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
            foreach (var resultAction in resultActions)
            {
                resultAction.Verb.Should().Be(GeneratePackingSlip);
            }
            resultActions[0].Arguments.ToArray()[0].Should().Be(Shipping);
            resultActions[1].Arguments.ToArray()[0].Should().Be(ForRoyaltyDepartment);
        }
                
        [TestMethod]
        public void TestMembershipActivation()
        {
            var owner = Substitute.For<IOwner>();
            owner.Name.Returns("John Doe");
            owner.Email.Returns("jd@mail.com");

            var membershipActivation = Substitute.For<IMembershipActivation>();
            membershipActivation.Owner.Returns(owner);
            membershipActivation.Name.Returns("Membership activation");

            var paymentItem = Substitute.For<IPaymentItem>();
            paymentItem.Product.Returns(membershipActivation);
            paymentItem.Amount.Returns(2.01);

            var payment = Substitute.For<IPayment>();
            payment.Date.Returns(DateTime.Now);
            payment.PaymentItems.Returns(new[] {paymentItem});

            var actionMembershipActivation = Substitute.For<IAction>();
            actionMembershipActivation.Verb.Returns(MembershipActivation);
            actionMembershipActivation.Arguments.Returns(new[] { Activate });

            var actions = Substitute.For<IActions>();
            actions.ActionCollection.Returns(new[] {actionMembershipActivation});

            var rule = Substitute.For<IRule>();
            rule.Execute().Returns(actions);

            var rules = Substitute.For<IRules>();
            rules.RulesCollection.Returns(new[] {rule});

            var rulesEngine = Substitute.For<RulesEngine.RulesEngine>(rules);
            rulesEngine.Execute(payment).Returns(actions);

            var result = rulesEngine.Execute(payment);

            result.ActionCollection.Count().Should().Be(1);
            result.ActionCollection.Should().ContainItemsAssignableTo<IAction>();
            var resultActions = result.ActionCollection.ToArray();
            resultActions[0].Verb.Should().Be(MembershipActivation);
            resultActions[0].Arguments.ToArray()[0].Should().Be(Activate);
        }
    }

}