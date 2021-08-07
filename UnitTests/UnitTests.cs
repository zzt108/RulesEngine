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
        const string GeneratePackingSlip = "Generate Packing Slip";
        const string Shipping = "Shipping";
        const string ForRoyaltyDepartment = "For Royalty Department";
        const string MembershipNotification = "Membership Notification";
        const string MembershipActivation = "Membership Activation";
        const string Activate = "Activate";
        const string MembershipUpgrade = "Membership Upgrade";
        const string Upgrade = "Upgrade";

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
            membershipActivation.Name.Returns(MembershipActivation);

            var paymentItem = Substitute.For<IPaymentItem>();
            paymentItem.Product.Returns(membershipActivation);
            paymentItem.Amount.Returns(2.01);

            var payment = Substitute.For<IPayment>();
            payment.Date.Returns(DateTime.Now);
            payment.PaymentItems.Returns(new[] {paymentItem});

            var actionMembershipActivation = Substitute.For<IAction>();
            actionMembershipActivation.Verb.Returns(MembershipActivation);
            actionMembershipActivation.Arguments.Returns(new[] { Activate });

            var actionMembershipNotification = Substitute.For<IAction>();
            actionMembershipNotification.Verb.Returns(MembershipNotification);
            var email = owner.Email;
            actionMembershipNotification.Arguments.Returns(new[] { Activate, email });

            var actions = Substitute.For<IActions>();
            actions.ActionCollection.Returns(new[]
                                                 {
                                                     actionMembershipActivation, 
                                                     actionMembershipNotification
                                                 });

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
            resultActions[0].Verb.Should().Be(MembershipActivation);
            resultActions[0].Arguments.ToArray()[0].Should().Be(Activate);
            resultActions[1].Verb.Should().Be(MembershipNotification);
            resultActions[1].Arguments.ToArray()[0].Should().Be(Activate);
            resultActions[1].Arguments.ToArray()[1].Should().Be(owner.Email);
        }

        [TestMethod]
        public void TestMembershipUpgrade()
        {
            var owner = Substitute.For<IOwner>();
            owner.Name.Returns("John Doe");
            owner.Email.Returns("jd@mail.com");

            var membershipUpgrade = Substitute.For<IMembershipUpgrade>();
            membershipUpgrade.Owner.Returns(owner);
            membershipUpgrade.Name.Returns(MembershipUpgrade);

            var paymentItem = Substitute.For<IPaymentItem>();
            paymentItem.Product.Returns(membershipUpgrade);
            paymentItem.Amount.Returns(0.01);

            var payment = Substitute.For<IPayment>();
            payment.Date.Returns(DateTime.Now);
            payment.PaymentItems.Returns(new[] { paymentItem });

            var actionMembershipUpgrade = Substitute.For<IAction>();
            actionMembershipUpgrade.Verb.Returns(MembershipUpgrade);
            actionMembershipUpgrade.Arguments.Returns(new[] { Upgrade });

            var actionMembershipNotification = Substitute.For<IAction>();
            actionMembershipNotification.Verb.Returns(MembershipNotification);
            var email = owner.Email;
            actionMembershipNotification.Arguments.Returns(new string[] { Upgrade, email });

            var actions = Substitute.For<IActions>();
            actions.ActionCollection.Returns(new[]
                                                 {
                                                     actionMembershipUpgrade,
                                                     actionMembershipNotification
                                                 });

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
            resultActions[0].Verb.Should().Be(MembershipUpgrade);
            resultActions[0].Arguments.ToArray()[0].Should().Be(Upgrade);
            resultActions[1].Verb.Should().Be(MembershipNotification);
            resultActions[1].Arguments.ToArray()[0].Should().Be(Upgrade);
            resultActions[1].Arguments.ToArray()[1].Should().Be(owner.Email);
        }
    }

}