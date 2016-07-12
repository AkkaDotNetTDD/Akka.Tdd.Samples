namespace Akka.Tdd.Sample
{
    public class TellFatherMessage
    {
        public TellFatherMessage(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}