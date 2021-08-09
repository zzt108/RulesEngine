﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using RulesEngine;

namespace UnitTests
{
    using System.Globalization;

    using NSubstitute.ReturnsExtensions;

    using RulesEngine = RulesEngine.RulesEngine;

    [TestClass]
    public class UnitTests
    {
        private static IActions Actions(IPhysicalProduct product)
        {
            var actions = Substitute.For<IActions>();
            var actionPackingSlip = Substitute.For<IAction>();
            actionPackingSlip.Verb.Returns(ActionConstants.GeneratePackingSlip);
            var productName = product.Name;
            actionPackingSlip.Arguments.Returns(new[] { ActionConstants.Shipping, productName });

            if (product.Agent != null)
            {
                var actionCommission = Substitute.For<IAction>();
                actionCommission.Verb.Returns(ActionConstants.CommissionPayment);
                var name = product.Agent.Name;
                var commission = product.Agent.Commission.ToString(CultureInfo.InvariantCulture);
                actionCommission.Arguments.Returns(new[] { name, commission });

                actions.ActionCollection.Returns(new[]
                                                     {
                                                         actionPackingSlip,
                                                         actionCommission
                                                     });
            }
            else
            {
                actions.ActionCollection.Returns(new[]
                                                     {
                                                         actionPackingSlip,
                                                     });

            }


            if (product is IBook)
            {
                var actionPackingSlipForBook = Substitute.For<IAction>();
                actionPackingSlipForBook.Verb.Returns(ActionConstants.GeneratePackingSlip);
                actionPackingSlipForBook.Arguments.Returns(new[] { ActionConstants.ForRoyaltyDepartment });
                var newActions = actions.ActionCollection.ToList();
                newActions.Add(actionPackingSlipForBook);
                actions.ActionCollection.Returns(newActions);
            }
            return actions;
        }

        
        [TestMethod]
        public void TestPhysicalProductRules()
        {
            var owner = Substitute.For<IOwner>();
            owner.Name.Returns("John Doe");
            owner.Email.Returns("jd@mail.com");

            var agent = Substitute.For<IAgent>();
            agent.Name.Returns("James Bond");
            agent.Commission.Returns(0.1);

            var physicalProduct = Substitute.For<IPhysicalProduct>();
            physicalProduct.Owner.Returns(owner);
            physicalProduct.Agent.Returns(agent);
            physicalProduct.Name.Returns("Screw driver");

            var paymentItem = Substitute.For<IPaymentItem>();
            paymentItem.Product.Returns(physicalProduct);
            paymentItem.Amount.Returns(1.01);

            var payment = Substitute.For<IPayment>();
            payment.Date.Returns(DateTime.Now);
            payment.PaymentItems.Returns(new[] { paymentItem });

            var actions = Actions(physicalProduct);

            var rule = Substitute.For<IRule>();
            rule.Execute(Arg.Any<IPaymentItem>()).Returns(actions);

            // Actual Implementation
            var rules = new Rules(new IRule[] { rule });

            //using actual implementation of RulesEngine
            var rulesEngine = new RulesEngine(rules);
            var result = rulesEngine.Execute(payment);

            /*
             * Actual tests
             */
            result.ActionCollection.Count().Should().Be(2);
            result.ActionCollection.Should().ContainItemsAssignableTo<IAction>();
            var resultActions = result.ActionCollection.ToArray();

            resultActions[0].Verb.Should().Be(ActionConstants.GeneratePackingSlip);
            resultActions[0].Arguments.ToArray()[0].Should().Be(ActionConstants.Shipping);
            resultActions[1].Verb.Should().Be(ActionConstants.CommissionPayment);
            resultActions[1].Arguments.ToArray()[0].Should().Be(agent.Name);
            resultActions[1].Arguments.ToArray()[1].Should().Be(agent.Commission.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestBookRules()
        {
            var owner = Substitute.For<IOwner>();
            owner.Name.Returns("John Doe");
            owner.Email.Returns("jd@mail.com");

            var agent = Substitute.For<IAgent>();
            agent.Name.Returns("James Bond");
            agent.Commission.Returns(0.1);

            var book = Substitute.For<IBook>();
            book.Owner.Returns(owner);
            book.Agent.Returns(agent);
            book.Name.Returns("Jungle Book");

            var paymentItem = Substitute.For<IPaymentItem>();
            paymentItem.Product.Returns(book);
            paymentItem.Amount.Returns(1.01);

            var payment = Substitute.For<IPayment>();
            payment.Date.Returns(DateTime.Now);
            payment.PaymentItems.Returns(new[] { paymentItem });

            var actions = Actions(book);

            var rule = Substitute.For<IRule>();
            rule.Execute(Arg.Any<IPaymentItem>()).Returns(actions);

            // Actual Implementation
            var rules = new Rules(new IRule[] { rule });

            //using actual implementation of RulesEngine
            var rulesEngine = new RulesEngine(rules);
            var result = rulesEngine.Execute(payment);

            /*
             * Actual tests
             */
            result.ActionCollection.Count().Should().Be(3);
            result.ActionCollection.Should().ContainItemsAssignableTo<IAction>();
            var resultActions = result.ActionCollection.ToArray();

            resultActions[0].Verb.Should().Be(ActionConstants.GeneratePackingSlip);
            resultActions[0].Arguments.ToArray()[0].Should().Be(ActionConstants.Shipping);
            resultActions[1].Verb.Should().Be(ActionConstants.CommissionPayment);
            resultActions[1].Arguments.ToArray()[0].Should().Be(agent.Name);
            resultActions[1].Arguments.ToArray()[1].Should().Be(agent.Commission.ToString(CultureInfo.InvariantCulture));
            resultActions[2].Verb.Should().Be(ActionConstants.GeneratePackingSlip);
            resultActions[2].Arguments.ToArray()[0].Should().Be(ActionConstants.ForRoyaltyDepartment);
        }

        [TestMethod]
        public void TestVideoRules()
        {
            var owner = Substitute.For<IOwner>();
            owner.Name.Returns("John Doe");
            owner.Email.Returns("jd@mail.com");

            var agent = Substitute.For<IAgent>();
            agent.Name.Returns("James Bond");
            agent.Commission.Returns(0.1);

            var video = Substitute.For<IVideo>();
            video.Owner.Returns(owner);
            video.Agent.Returns(agent);
            video.Name.Returns("Blown by the wind");

            var paymentItem = Substitute.For<IPaymentItem>();
            paymentItem.Product.Returns(video);
            paymentItem.Amount.Returns(1.01);

            var payment = Substitute.For<IPayment>();
            payment.Date.Returns(DateTime.Now);
            payment.PaymentItems.Returns(new[] { paymentItem });

            var actions = Actions(video);

            var rule = Substitute.For<IRule>();
            rule.Execute(Arg.Any<IPaymentItem>()).Returns(actions);

            // Actual implementation
            var rules = new Rules(new IRule[] { rule });

            // using actual implementation of RulesEngine
            var rulesEngine = new RulesEngine(rules);
            var result = rulesEngine.Execute(payment);

            // Actual tests
            result.ActionCollection.Count().Should().Be(2);
            result.ActionCollection.Should().ContainItemsAssignableTo<IAction>();
            var resultActions = result.ActionCollection.ToArray();

            resultActions[0].Verb.Should().Be(ActionConstants.GeneratePackingSlip);
            resultActions[0].Arguments.ToArray()[0].Should().Be(ActionConstants.Shipping);
            resultActions[1].Verb.Should().Be(ActionConstants.CommissionPayment);
            resultActions[1].Arguments.ToArray()[0].Should().Be(agent.Name);
            resultActions[1].Arguments.ToArray()[1].Should().Be(agent.Commission.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestLearningToSkiRules()
        {
            var owner = Substitute.For<IOwner>();
            owner.Name.Returns("John Doe");
            owner.Email.Returns("jd@mail.com");

            var agent = Substitute.For<IAgent>();
            agent.Name.Returns("James Bond");
            agent.Commission.Returns(0.1);

            var videoLTS = Substitute.For<IVideo>();
            videoLTS.Owner.Returns(owner);
            videoLTS.Agent.Returns(agent);
            videoLTS.Name.Returns(ActionConstants.LearningToSki);

            var videoFA = Substitute.For<IVideo>();
            videoFA.Owner.Returns(owner);
            videoFA.Agent.ReturnsNull();
            videoFA.Name.Returns(ActionConstants.FirstAid);

            var paymentItem = Substitute.For<IPaymentItem>();
            paymentItem.Product.Returns(videoLTS);
            paymentItem.Amount.Returns(1.01);

            var payment = Substitute.For<IPayment>();
            payment.Date.Returns(DateTime.Now);
            payment.PaymentItems.Returns(new[] { paymentItem });

            var actions = Actions(videoLTS);
            var actions2 = Actions(videoFA);
            var actionCollection = actions.ActionCollection.ToList();
            actionCollection.AddRange(actions2.ActionCollection);
            actions.ActionCollection.Returns(actionCollection);

            var rule = Substitute.For<IRule>();
            rule.Execute(Arg.Any<IPaymentItem>()).Returns(actions);

            // Actual implementation
            var rules = new Rules(new IRule[] { rule });

            //using actual implementation of RulesEngine
            var rulesEngine = new RulesEngine(rules);
            var result = rulesEngine.Execute(payment);

            // Actual tests
            result.ActionCollection.Count().Should().Be(3);
            result.ActionCollection.Should().ContainItemsAssignableTo<IAction>();
            var resultActions = result.ActionCollection.ToArray();

            resultActions[0].Verb.Should().Be(ActionConstants.GeneratePackingSlip);
            resultActions[0].Arguments.ToArray()[0].Should().Be(ActionConstants.Shipping);
            resultActions[1].Verb.Should().Be(ActionConstants.CommissionPayment);
            resultActions[1].Arguments.ToArray()[0].Should().Be(agent.Name);
            resultActions[1].Arguments.ToArray()[1].Should().Be(agent.Commission.ToString(CultureInfo.InvariantCulture));
            resultActions[2].Verb.Should().Be(ActionConstants.GeneratePackingSlip);
            resultActions[2].Arguments.ToArray()[0].Should().Be(ActionConstants.Shipping);
        }

        [TestMethod]
        public void TestMembershipActivation()
        {
            var owner = Substitute.For<IOwner>();
            owner.Name.Returns("John Doe");
            owner.Email.Returns("jd@mail.com");

            var membershipActivation = Substitute.For<IMembershipActivation>();
            membershipActivation.Owner.Returns(owner);
            membershipActivation.Name.Returns(ActionConstants.MembershipActivation);

            var paymentItem = Substitute.For<IPaymentItem>();
            paymentItem.Product.Returns(membershipActivation);
            paymentItem.Amount.Returns(2.01);

            var payment = Substitute.For<IPayment>();
            payment.Date.Returns(DateTime.Now);
            payment.PaymentItems.Returns(new[] { paymentItem });

            var actionMembershipActivation = Substitute.For<IAction>();
            actionMembershipActivation.Verb.Returns(ActionConstants.MembershipActivation);
            actionMembershipActivation.Arguments.Returns(new[] { ActionConstants.Activate });

            var actionMembershipNotification = Substitute.For<IAction>();
            actionMembershipNotification.Verb.Returns(ActionConstants.MembershipNotification);
            var email = owner.Email;
            actionMembershipNotification.Arguments.Returns(new[] { ActionConstants.Activate, email });

            var actions = Substitute.For<IActions>();
            actions.ActionCollection.Returns(new[]
                                                 {
                                                     actionMembershipActivation,
                                                     actionMembershipNotification
                                                 });

            var rule = Substitute.For<IRule>();
            rule.Execute(Arg.Any<IPaymentItem>()).Returns(actions);

            // Actual implementation
            var rules = new Rules(new IRule[] { rule });


            //using actual implementation of RulesEngine
            var rulesEngine = new RulesEngine(rules);
            var result = rulesEngine.Execute(payment);

            // Actual tests
            result.ActionCollection.Count().Should().Be(2);
            result.ActionCollection.Should().ContainItemsAssignableTo<IAction>();
            var resultActions = result.ActionCollection.ToArray();
            resultActions[0].Verb.Should().Be(ActionConstants.MembershipActivation);
            resultActions[0].Arguments.ToArray()[0].Should().Be(ActionConstants.Activate);
            resultActions[1].Verb.Should().Be(ActionConstants.MembershipNotification);
            resultActions[1].Arguments.ToArray()[0].Should().Be(ActionConstants.Activate);
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
            membershipUpgrade.Name.Returns(ActionConstants.MembershipUpgrade);

            var paymentItem = Substitute.For<IPaymentItem>();
            paymentItem.Product.Returns(membershipUpgrade);
            paymentItem.Amount.Returns(0.01);

            var payment = Substitute.For<IPayment>();
            payment.Date.Returns(DateTime.Now);
            payment.PaymentItems.Returns(new[] { paymentItem });

            var actionMembershipUpgrade = Substitute.For<IAction>();
            actionMembershipUpgrade.Verb.Returns(ActionConstants.MembershipUpgrade);
            actionMembershipUpgrade.Arguments.Returns(new[] { ActionConstants.Upgrade });

            var actionMembershipNotification = Substitute.For<IAction>();
            actionMembershipNotification.Verb.Returns(ActionConstants.MembershipNotification);
            var email = owner.Email;
            actionMembershipNotification.Arguments.Returns(new string[] { ActionConstants.Upgrade, email });

            var actions = Substitute.For<IActions>();
            actions.ActionCollection.Returns(new[]
                                                 {
                                                     actionMembershipUpgrade,
                                                     actionMembershipNotification
                                                 });

            var rule = Substitute.For<IRule>();
            rule.Execute(Arg.Any<IPaymentItem>()).Returns(actions);

            // Actual implementation
            var rules = new Rules(new IRule[] { rule });

            //using actual implementation of RulesEngine
            var rulesEngine = new RulesEngine(rules);
            var result = rulesEngine.Execute(payment);

            // Actual tests
            result.ActionCollection.Count().Should().Be(2);
            result.ActionCollection.Should().ContainItemsAssignableTo<IAction>();
            var resultActions = result.ActionCollection.ToArray();
            resultActions[0].Verb.Should().Be(ActionConstants.MembershipUpgrade);
            resultActions[0].Arguments.ToArray()[0].Should().Be(ActionConstants.Upgrade);
            resultActions[1].Verb.Should().Be(ActionConstants.MembershipNotification);
            resultActions[1].Arguments.ToArray()[0].Should().Be(ActionConstants.Upgrade);
            resultActions[1].Arguments.ToArray()[1].Should().Be(owner.Email);
        }
    }
}