using System.Threading.Tasks;

namespace ATM.Application.Interfaces.Repositories
{
    public interface ICashManagementRepository
    {
        Task<int> GetAvailableCashAsync(); // Ahora devuelve un Task
        Task LoadCashAsync(int amount);    // Método asincrónico
    }
}
