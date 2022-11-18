namespace Sovcombank.FinancialTrading.EventSourcing;

public interface IInternalEventHandler
{
    void Handle(IEvent @event);
}
