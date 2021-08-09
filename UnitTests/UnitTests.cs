using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using RulesEngine;
using System.Globalization;
using NSubstitute.ReturnsExtensions;

namespace UnitTests
{
    using RulesEngine = RulesEngine.RulesEngine;

    [TestClass]
    public class UnitTests
    {

        private IEnumerable<IRule> getAllRules()
        {
            return new IRule[]
                       {
                           new RulePhysicalProduct(),
                           new RulePhysicalProductAgentCommission(),
                           new RuleBook(),
                           new RuleMembershipActivation(),
                           new RuleMembershipUpgrade(),
                           new RuleMembershipNotificatio(),
                           new RuleVideoLearningToSki(),
                       };
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


            // Actual Implementation
            var rules = new Rules(this.getAllRules());

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

            // Actual Implementation
            var rules = new Rules(this.getAllRules());

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

            // Actual implementation
            var rules = new Rules(this.getAllRules());

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

            // Actual implementation
            var rules = new Rules(this.getAllRules());

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

            // Actual implementation
            var rules = new Rules(this.getAllRules());


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

            // Actual implementation
            var rules = new Rules(this.getAllRules());

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