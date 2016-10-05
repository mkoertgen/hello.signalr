namespace hello.signalr
{
    public interface IChatHub
    {
        void Send(string name, string message);
    }
}