namespace Domain.Events.RoomEvent;

public class MessagesRoomCreatedEvent : BaseEvent
{
    public MessagesRoomCreatedEvent(RoomMessages room)
    {
        Room = room;
    }

    public RoomMessages Room { get; }
}
