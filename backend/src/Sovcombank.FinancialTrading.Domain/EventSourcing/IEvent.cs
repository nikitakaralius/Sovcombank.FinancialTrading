namespace Sovcombank.FinancialTrading.Domain.EventSourcing;

public interface IEvent { }

public interface IEvent<out T> { }
