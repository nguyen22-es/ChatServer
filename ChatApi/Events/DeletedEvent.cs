namespace Domain.Events;

public class DeletedEvent<T> : BaseEvent
{
    public DeletedEvent(T item)
    {
        Item = item;
    }

    public T Item { get; }
}
