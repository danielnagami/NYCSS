using NYCSS.Utils.MessageBus.Messages;

namespace NYCSS.Domain.Entities
{
    public abstract class Entity
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        protected Entity()
        {
            ID = Guid.NewGuid();
        }

        private List<Event>? _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        public void AddEvent(Event @event)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(@event);
        }

        public void RemoveEvent(Event eventItem)
        {
            _notifications?.Remove(eventItem);
        }

        public void ClearEvents()
        {
            _notifications?.Clear();
        }
    }
}