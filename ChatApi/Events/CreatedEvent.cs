namespace Domain.Events;

public class CreatedEvent<T> : BaseEvent
{
    public CreatedEvent(T item)
    {
        Item = item;
    }

    public T Item { get; }
}
