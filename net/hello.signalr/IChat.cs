namespace hello.signalr
{
    public interface IChat
    {
        void BroadcastMessage(string name, string message);
    }
}