﻿using System.Threading.Tasks;
using Akka.Actor;
using Akka.Tdd.AutoFac;
using Akka.Tdd.Core;
using Akka.Tdd.TestKit;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akka.Tdd.SandBox
{
    [TestClass]
    public class When_brother_actor_talks_to_his_father
    {
        [TestMethod]
        public void his_father_should_reply_back_to_him()
        {
            //Arrange
            var container = new ContainerBuilder().Build();
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver(container));
            var factory = new TddTestKitfactoryFactory(container, system.ActorSystem);

            factory.WhenActorReceives<TellFatherMessage>().ItShouldDo(actor =>
            {
                ((ICanTell) actor.Context.Sender).Tell(new GeneralMessage("My name is " + GetType().Name + " and I got a message"), actor.Context.Sender);
            }).SetUpMockActor<MockActor>();

            IActorRef factherActor = null;
            var brother = factory.WhenActorStarts().ItShouldDo(actor =>
            {
                factherActor = actor.Context.System.CreateActor(actor.ActorChildrenOrDependencies.Item1.ActorType);
            }).WhenActorReceives<TellFatherMessage>().ItShouldDo(actor =>
            {
                factherActor.Tell(new GeneralMessage("My name is " + GetType().Name + " and I got a message"));
                ((ICanTell) actor.Context.Sender).Tell(new TellFatherCompletedMessage(true), actor.Context.Sender);
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