namespace ZulZula
{
    public interface ILogWriter
    {
        void Write(string message);
    }

    public class EmptyLogWriter : ILogWriter
    {
        public void Write(string message)
        {
        }
    }
}
