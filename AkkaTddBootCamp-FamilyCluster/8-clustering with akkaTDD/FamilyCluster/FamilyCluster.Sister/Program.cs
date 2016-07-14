using System;
using System.Configuration;
using Akka.Actor;
using Akka.Routing;
using Akka.Tdd.AutoFac;
using Akka.Tdd.Core;
using FamilyCluster.Common;

namespace FamilyCluster.Sister
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting SisterSystem ...");

            var system = new ApplicationActorSystem();
            system.RegisterAndCreateActorSystem(new AutoFacAkkaDependencyResolver(), "FamilyCluster");

            var sisterEchoActor = system.ActorSystem.CreateActor<SisterEchoActor>(new ActorSetUpOptions()
            {
                RouterConfig = FromConfig.Instance
            });
            while (true)
            {
                var message = Console.ReadLine();
                sisterEchoActor.Tell(new Hello("From SisterSystem to sisterEchoActor" + message), ActorRefs.NoSender);
            }

        }
    }
}