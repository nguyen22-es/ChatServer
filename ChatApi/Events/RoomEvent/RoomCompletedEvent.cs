namespace Domain.Events.RoomEvent;

public class RoomCompletedEvent : BaseEvent
{
    public RoomCompletedEvent(Rooms room)
    {
       Room = room;
        
    }

    public Rooms Room { get; }
}
