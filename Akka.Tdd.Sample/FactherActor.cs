using Akka.Actor;

namespace Akka.Tdd.Sample
{
    public class FatherActor : ReceiveActor
    {
        public FatherActor()
        {
            Receive<TellFatherMessage>(message =>
            {
                Sender.Tell(new GeneralMessage("My name is " + GetType().Name + " and I got a message which says " + message.Message));
            });
            ReceiveAny(message =>
            {
                //log
            });
        }
    }
}