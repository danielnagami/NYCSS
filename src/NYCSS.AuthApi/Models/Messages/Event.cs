using MediatR;

namespace NYCSS.AuthApi.Models.Messages
{
    public class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }
        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}