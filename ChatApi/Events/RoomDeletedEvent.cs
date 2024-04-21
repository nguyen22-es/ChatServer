

namespace ChatSrever.Domain.Events;

public class TodoItemDeletedEvent : BaseEvent
{
    public TodoItemDeletedEvent(Rooms room)
    {
        Room = room;
    }

    public Rooms Room { get; }
}
