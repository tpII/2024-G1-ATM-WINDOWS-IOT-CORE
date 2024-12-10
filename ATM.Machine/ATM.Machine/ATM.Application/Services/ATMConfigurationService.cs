using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.Interfaces.Services;

namespace ATM.Application.Services
{
    public class ATMConfigurationService : IATMConfigurationService
    {
        private readonly IATMConfigurationRepository _repository;

        public ATMConfigurationService(IATMConfigurationRepository repository)
        {
            _repository = repository;
        }

        public async Task SetWithdrawalLimitsAsync(int maxLimit, int minLimit)
        {
            await _repository.SetWithdrawalLimitsAsync(maxLimit, minLimit);
        }

        public async Task<(int MaxLimit, int MinLimit)> GetWithdrawalLimitsAsync()
        {
            return await _repository.GetWithdrawalLimitsAsync();
        }
    }
}
