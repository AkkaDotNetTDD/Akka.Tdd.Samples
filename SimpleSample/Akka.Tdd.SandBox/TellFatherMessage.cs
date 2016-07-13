namespace Akka.Tdd.SandBox
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