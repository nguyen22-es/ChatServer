namespace ChatServer.Domain.Events;

public class UserCompletedEvent : BaseEvent
{
    public UserCompletedEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
