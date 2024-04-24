namespace Domain.Events;

public class CompletedEvent : BaseEvent
{
    public CompletedEvent(Messages item)
    {
        Item = item;
    }

    public Messages Item { get; }
}
