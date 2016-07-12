namespace Akka.Tdd.SandBox
{
    public class TellFatherCompletedMessage
    {
        public TellFatherCompletedMessage(bool isSuccessfull)
        {
            IsSuccessfull = isSuccessfull;
        }

        public bool IsSuccessfull { private set; get; }
    }
}