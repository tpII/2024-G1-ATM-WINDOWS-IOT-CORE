using System.Threading.Tasks;

namespace ATM.Application.Interfaces.Services
{
    public interface ICashManagementService
    {
        Task<decimal> GetAvailableCashAsync();
        Task DispenseCashAsync(decimal amount);
        Task LoadCashAsync(decimal amount);
    }
}
