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
            if(minLimit < 0 || maxLimit < 0)
            {
                throw ArgumentOutOfRangeException("Los límites deben ser mayores o iguales a cero");
            }
            if(maxLimit < minLimit) {
                throw ArgumentException("El limite máximo debe ser mayor o igual al límite mínimo");
            }
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
