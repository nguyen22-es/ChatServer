namespace Domain.Events.RoomEvent;

public class RoomCreatedEvent : BaseEvent
{
    public RoomCreatedEvent(Rooms room)
    {
        Room = room;
    }

    public Rooms Room { get; }
}
