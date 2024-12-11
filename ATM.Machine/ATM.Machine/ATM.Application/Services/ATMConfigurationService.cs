using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.Interfaces.Services;

namespace ATM.Application.Services
{
    public class ATMConfigurationService : IATMConfigurationService
    {
        public decimal MaxLimit { get; private set; } = 1000000;
        public decimal MinLimit { get; private set; }

        public Task SetWithdrawalLimitsAsync(decimal maxLimit, decimal minLimit)
        {
            MaxLimit = maxLimit;
            MinLimit = minLimit;
            return Task.CompletedTask;
        }
        
        public Task<(decimal MaxLimit, decimal MinLimit)> GetWithdrawalLimitsAsync()
        {
            return Task.FromResult((MaxLimit, MinLimit));
        }

    }
}
