namespace Domain.Events.MessagesRoom;

public class MessagesRoomCompletedEvent : BaseEvent
{
    public MessagesRoomCompletedEvent(RoomMessages room)
    {
        Room = room;
    }

    public RoomMessages Room { get; }
}
