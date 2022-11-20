namespace Sovcombank.FinancialTrading.Application.Interfaces;

public interface ICurrentUserService
{
    Guid? UserId { get; }
}
