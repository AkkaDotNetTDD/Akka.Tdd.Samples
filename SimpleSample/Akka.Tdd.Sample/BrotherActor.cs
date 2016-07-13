using Akka.Actor;
using Akka.Tdd.Core;

namespace Akka.Tdd.Sample
{
    public class BrotherActor<TFatherActor> : ReceiveActor where TFatherActor : ActorBase
    {
        public BrotherActor()
        {
            var FactherActor = Context.System.CreateActor<TFatherActor>();
            Receive<TellFatherMessage>(message =>
            {
                FactherActor.Tell(message);
                Sender.Tell(new TellFatherCompletedMessage(true));
            });
            ReceiveAny(message =>
            {
                Sender.Tell(new GeneralMessage("My name is " + GetType().Name + " and I got a message from " + Sender.Path));
            });
        }
    }
}