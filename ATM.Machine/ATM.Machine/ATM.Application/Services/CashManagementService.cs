using ATM.Application.Interfaces.Repositories;
using ATM.Application.Interfaces.Services;

namespace ATM.Application.Services
{
    public class CashManagementService : ICashManagementService
    {
        private readonly ICashManagementRepository _cashManagementRepository;

        public CashManagementService(ICashManagementRepository cashManagementRepository)
        {
            _cashManagementRepository = cashManagementRepository;
        }

        public int GetAvailableCash()
        {
            return _cashManagementRepository.GetAvailableCash();
        }

        public void LoadCash(int amount)
        {
            _cashManagementRepository.LoadCash(amount);
        }
    }
}
