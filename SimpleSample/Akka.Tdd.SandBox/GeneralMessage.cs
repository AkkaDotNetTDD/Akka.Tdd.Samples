namespace Akka.Tdd.SandBox
{
    public class GeneralMessage
    {
        public GeneralMessage(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}