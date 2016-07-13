using System.Threading.Tasks;
using Akka.Actor;
using Akka.Tdd.AutoFac;
using Akka.Tdd.Core;
using Akka.Tdd.TestKit;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akka.Tdd.Sample.Tests
{
    [TestClass]
    public class When_brother_actor_talks_to_his_father : Akka.TestKit.Xunit.TestKit
    {
       

        [TestMethod]
        public void his_father_should_reply_back_to_him1()
        {
            //Arrange
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver());
            var factory = new TddTestKitfactoryFactory(null, system.ActorSystem);

            //Act
            var brother = system.ActorSystem.CreateActor<BrotherActor<FatherActor>>();
            brother.Tell(new TellFatherMessage("Hello"));

            //Assert
            var result = factory.AwaitAssert(() => ExpectMsg<TellFatherCompletedMessage>().IsSuccessfull, 3000);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void his_father_should_reply_back_to_him2()
        {
            //Arrange
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver());
            var factory = new TddTestKitfactoryFactory(null, system.ActorSystem);

            //Act
            var father = system.ActorSystem.CreateActor<FatherActor>();
            father.Tell(new TellFatherMessage("Hello"));

            //Assert
            var result = factory.AwaitAssert(() => ExpectMsg<GeneralMessage>().Message, 3000);
            Assert.AreEqual("My name is FatherActor and I got a message which says Hello", result);
        }

        [TestMethod]
        public void his_father_should_reply_back_to_him3()
        {
            //Arrange
            var container = new ContainerBuilder().Build();
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver(container));
            var factory = new TddTestKitfactoryFactory(container, system.ActorSystem);

            //Act
            var brother = factory.CreateActor<BrotherActor<FatherActor>>();
            brother.Tell(new TellFatherMessage("Hello"));

            //Assert
            var result = factory.AwaitAssert(() => ExpectMsg<TellFatherCompletedMessage>().IsSuccessfull, 3000);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void his_father_should_reply_back_to_him4()
        {
            //Arrange
            var container = new ContainerBuilder().Build();
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver(container));
            var factory = new TddTestKitfactoryFactory(container, system.ActorSystem);

            factory.WhenActorReceives<TellFatherMessage>().ItShouldDo(actor =>
            {
                actor.Context.Sender.Tell(new GeneralMessage("My name is " + GetType().Name + " and I got a message"));
            }).SetUpMockActor<MockActor>();

            //Act
            var brother = factory.CreateActor<BrotherActor<MockActor>>();
            brother.Tell(new TellFatherMessage("Hello"));

            //Assert
            var result = factory.AwaitAssert(() => ExpectMsg<TellFatherCompletedMessage>().IsSuccessfull, 3000);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void his_father_should_reply_back_to_him5()
        {
            //Arrange
            var container = new ContainerBuilder().Build();
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver(container));
            var factory = new TddTestKitfactoryFactory(container, system.ActorSystem);

            IActorRef factherActor = null;
            var brother = factory.WhenActorStarts().ItShouldDo(actor =>
            {
                factherActor = actor.Context.System.CreateActor(actor.ActorChildrenOrDependencies.Item1.ActorType);
            }).WhenActorReceives<TellFatherMessage>().ItShouldDo(actor =>
            {
                factherActor.Tell(new GeneralMessage("My name is " + GetType().Name + " and I got a message"));
                actor.Context.Sender.Tell(new TellFatherCompletedMessage(true));
            }).CreateMockActorRef<MockActor<FatherActor>>();

            //Act
            brother.Tell(new TellFatherMessage("Hello"));

            //Assert
            var result = factory.AwaitAssert(() => ExpectMsg<TellFatherCompletedMessage>().IsSuccessfull, 3000);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void his_father_should_reply_back_to_him6()
        {
            //Arrange
            var container = new ContainerBuilder().Build();
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver(container));
            var factory = new TddTestKitfactoryFactory(container, system.ActorSystem);

            factory.WhenActorReceives<TellFatherMessage>().ItShouldDo(actor =>
            {
                actor.Context.Sender.Tell(new GeneralMessage("My name is " + GetType().Name + " and I got a message"));
            }).SetUpMockActor<MockActor>();

            IActorRef factherActor = null;
            var brother = factory.WhenActorStarts().ItShouldDo(actor =>
            {
                factherActor = actor.Context.System.CreateActor(actor.ActorChildrenOrDependencies.Item1.ActorType);
            }).WhenActorReceives<TellFatherMessage>().ItShouldDo(actor =>
            {
                factherActor.Tell(new GeneralMessage("My name is " + GetType().Name + " and I got a message"));
                actor.Context.Sender.Tell(new TellFatherCompletedMessage(true));
            }).CreateMockActorRef<MockActor<MockActor>>();

            //Act
            brother.Tell(new TellFatherMessage("Hello"));

            //Assert
            var result = factory.AwaitAssert(() => ExpectMsg<TellFatherCompletedMessage>().IsSuccessfull, 3000);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void his_father_should_reply_back_to_him7()
        {
            //Arrange
            var container = new ContainerBuilder().Build();
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver(container));
            var factory = new TddTestKitfactoryFactory(container, system.ActorSystem);

            var father = factory.WhenActorReceives<TellFatherMessage>().ItShouldDo(actor =>
            {
                actor.Context.Sender.Tell(new GeneralMessage("My name is " + GetType().Name + " and I got a message"));
            }).CreateMockActorRef<MockActor>();

            //Act
            father.Tell(new TellFatherMessage("Hello"));

            //Assert
            var result = factory.AwaitAssert(() => ExpectMsg<GeneralMessage>().Message, 3000);
            Assert.AreEqual("My name is When_brother_actor_talks_to_his_father and I got a message", result);
        }

        [TestMethod]
        public void his_father_should_reply_back_to_him8()
        {
            //Arrange
            var container = new ContainerBuilder().Build();
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver(container));
            var factory = new TddTestKitfactoryFactory(container, system.ActorSystem);

            var father = factory.WhenActorReceives<TellFatherMessage>().ItShouldDo(actor =>
            {
                actor.Context.Sender.Tell(new GeneralMessage("My name is " + GetType().Name + " and I got a message"));
            }).CreateMockActorRef<MockActor>();

            //Act
            var task = father.Ask(new TellFatherMessage("Hello"));
            Task.WaitAll(task);
            var result = task.Result as GeneralMessage;

            //Assert
            Assert.AreEqual("My name is When_brother_actor_talks_to_his_father and I got a message", result?.Message);
        }

        [TestMethod]
        public void his_father_should_reply_back_to_him9()
        {
            //Arrange
            var container = new ContainerBuilder().Build();
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver(container));
            var factory = new TddTestKitfactoryFactory(container, system.ActorSystem);

            factory.WhenActorReceives<TellFatherMessage>().ItShouldDo(actor =>
            {
                actor.Context.Sender.Tell(new GeneralMessage("My name is " + GetType().Name + " and I got a message"));
            }).SetUpMockActor<MockActor>();

            IActorRef factherActor = null;
            var brother = factory.WhenActorStarts().ItShouldDo(actor =>
            {
                factherActor = actor.Context.System.CreateActor(actor.ActorChildrenOrDependencies.Item1.ActorType);
            }).WhenActorReceives<TellFatherMessage>().ItShouldDo(actor =>
            {
                factherActor.Tell(new GeneralMessage("My name is " + GetType().Name + " and I got a message"));
                actor.Context.Sender.Tell(new TellFatherCompletedMessage(true));
            }).CreateMockActorRef<MockActor<MockActor>>();

            //Act
            var task = brother.Ask(new TellFatherMessage("Hello"));
            Task.WaitAll(task);
            var result = task.Result as TellFatherCompletedMessage;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessfull);
        }
    }
}