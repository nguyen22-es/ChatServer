namespace Domain.Events;

public class CompletedEvent<T> : BaseEvent
{
    public CompletedEvent(T item)
    {
        Item = item;
    }

    public T Item { get; }
}
