using System;
using System.Configuration;
using Akka.Actor;
using Akka.Routing;
using Akka.Tdd.AutoFac;
using Akka.Tdd.Core;
using FamilyCluster.Common;

namespace FamilyCluster.Brother
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting BrotherSystem ...");
            var system = new ApplicationActorSystem();
            system.RegisterAndCreateActorSystem(new AutoFacAkkaDependencyResolver(), "FamilyCluster");
            var brotherEchoActor = system.ActorSystem.CreateActor<BrotherEchoActor>(new ActorSetUpOptions()
            {
                RouterConfig = FromConfig.Instance
            });

            while (true)
            {
                var message = Console.ReadLine();
                brotherEchoActor.Tell(new Hello("From BrotherSystem to BrotherEchoActor" + message), ActorRefs.NoSender);
            }

        }
    }
}