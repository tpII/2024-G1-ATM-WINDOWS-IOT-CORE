using System;
using System.Threading;
using System.Threading.Tasks;
using ATM.Core.Services;

namespace ATM.Core.UseCases 
{
    public class DepositUseCase
    {
        private ApiService Api { get; set; }

        public DepositUseCase(ApiService api) 
        {
            Api = api;
        }

        public async Task<bool> Execute(decimal amount)
        {
            if(amount <= 0)
            {
                throw new ArgumentException($"{nameof(amount)} must be greater than 0", nameof(amount));
            }
            return await Api.DepositAsync(amount);
        }
    }
}