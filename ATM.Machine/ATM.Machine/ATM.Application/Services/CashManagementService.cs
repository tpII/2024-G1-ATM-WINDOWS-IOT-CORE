using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.Interfaces.Services;

namespace ATM.Application.Services
{
    public class CashManagementService : ICashManagementService
    {
        private readonly ICashManagementRepository _repository;

        public CashManagementService(ICashManagementRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> GetAvailableCashAsync()
        {
            return await _repository.GetAvailableCashAsync();
        }

        public async Task LoadCashAsync(int amount)
        {
            await _repository.LoadCashAsync(amount);
        }
    }
}
