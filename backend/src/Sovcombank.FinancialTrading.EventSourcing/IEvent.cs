namespace Sovcombank.FinancialTrading.EventSourcing;

public interface IEvent { }

public interface IEvent<out T> : IEvent { }
