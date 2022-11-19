using Sovcombank.FinancialTrading.Domain.Common;
using Sovcombank.FinancialTrading.Domain.UserProfile.ValueObjects;

namespace Sovcombank.FinancialTrading.Domain.UserProfile;

public interface IUserProfile
{
    UserId Id { get; }

    UserFirstname Firstname { get; }

    UserLastname Lastname { get; }

    UserPatronymic Patronymic { get; }

    EmailAddress Email { get; }
}
