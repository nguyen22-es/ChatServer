namespace Domain.Events.RoomEvent;

public class RoomDeletedEvent : BaseEvent
{
    public RoomDeletedEvent(Rooms room)
    {
        Room = room;
    }

    public Rooms Room { get; }
}
