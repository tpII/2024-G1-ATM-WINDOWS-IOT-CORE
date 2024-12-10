using System.Threading.Tasks;

namespace ATM.Application.Interfaces.Services
{
    public interface ICashManagementService
    {
        Task<int> GetAvailableCashAsync();
        Task LoadCashAsync(int amount);
    }
}
