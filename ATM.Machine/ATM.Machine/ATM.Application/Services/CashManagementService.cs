using System.Threading.Tasks;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.Interfaces.Services;

namespace ATM.Application.Services
{
    public class CashManagementService : ICashManagementService
    {
        public decimal AvailableCash { get; private set; }

        public CashManagementService()
        {
            AvailableCash = 0;
        }

        public Task<decimal> GetAvailableCashAsync()
        {
            return Task.FromResult<decimal>(AvailableCash);
        }

        public Task DispenseCashAsync(decimal amount)
        {
            AvailableCash -= amount;
            return Task.CompletedTask;
        }

        public Task LoadCashAsync(decimal amount)
        {
            AvailableCash += amount;
            //USE repository to load cash
            return Task.CompletedTask;
        }
    }
}
