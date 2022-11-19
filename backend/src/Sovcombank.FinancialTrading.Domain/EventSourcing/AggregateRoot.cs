namespace Sovcombank.FinancialTrading.Domain.EventSourcing;

public abstract class AggregateRoot
{
    private readonly List<IEvent> _changes = new();

    public int Version { get; private set; } = -1;

    public IEnumerable<IEvent> Changes() => _changes;

    public void ClearChanges() => _changes.Clear();

    public void Load(IEnumerable<IEvent> history)
    {
        foreach (var e in history)
        {
            When(e);
            Version++;
        }
    }

    protected abstract void When(IEvent eventHappened);

    protected abstract void EnsureValidState();

    protected void Apply(IEvent @event)
    {
        When(eventHappened: @event);
        EnsureValidState();
        _changes.Add(@event);
    }
}
