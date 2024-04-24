namespace Domain.Events.RoomEvent;

public class MessagesRoomDeletedEvent : BaseEvent
{
    public MessagesRoomDeletedEvent(RoomMessages room)
    {
        Room = room;
    }

    public RoomMessages Room { get; }
}
