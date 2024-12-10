using System.Threading.Tasks;

namespace ATM.Application.Interfaces.Services
{
    public interface IATMConfigurationService
    {
        Task SetWithdrawalLimitsAsync(int maxLimit, int minLimit);
        Task<(int MaxLimit, int MinLimit)> GetWithdrawalLimitsAsync();
    }
}
