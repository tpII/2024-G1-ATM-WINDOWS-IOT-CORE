using System.Threading.Tasks;

namespace ATM.Application.Interfaces.Repositories
{
    public interface IATMConfigurationRepository
    {
        Task SetWithdrawalLimitsAsync(int maxLimit, int minLimit);
        Task<(int MaxLimit, int MinLimit)> GetWithdrawalLimitsAsync();
    }
}
