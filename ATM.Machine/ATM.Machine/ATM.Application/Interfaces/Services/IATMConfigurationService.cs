using System.Threading.Tasks;

namespace ATM.Application.Interfaces.Services
{
    public interface IATMConfigurationService
    {
        Task SetWithdrawalLimitsAsync(decimal maxLimit, decimal minLimit);
        Task<(decimal MaxLimit, decimal MinLimit)> GetWithdrawalLimitsAsync();
    }
}
