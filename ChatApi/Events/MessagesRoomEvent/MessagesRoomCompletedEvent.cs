namespace Domain.Events.MessagesRoom;

public class MessagesRoomCompletedEvent<T> : BaseEvent
{
    public MessagesRoomCompletedEvent(T room)
    {
        Room = room;
    }

    public T Room { get; }
}
