using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Routing;
using Akka.Tdd.AutoFac;
using Akka.Tdd.Core;
using FamilyCluster.Common;

namespace FamilyCluster.FamilyFriend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting FamilyFriendSyatem ...");
            var client = ConfigurationManager.AppSettings["client"];
            var system = new ApplicationActorSystem();
            system.RegisterAndCreateActorSystem(new AutoFacAkkaDependencyResolver(), "FamilyFriendSyatem");
            
                while (true)
                {
                    var message = Console.ReadLine();
                    var clientActor = system.ActorSystem.ActorSelection(client);
                    clientActor.Tell(new Hello("From FamilyFriendSyatem to client "+client+" : " + message), ActorRefs.NoSender);
                }
            
        }
    }
}
