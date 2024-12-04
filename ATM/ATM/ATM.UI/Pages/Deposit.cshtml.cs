using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ATM.UI.Pages
{
    public class DepositModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(decimal amount)
        {
            DepositResponse depositResult = await SendDepositRequestAsync(amount);
            Console.WriteLine(depositResult);
            Message = depositResult.ToString();

            return Page();
        }

        private async Task<DepositResponse> SendDepositRequestAsync(decimal sent_amount)
        {
            using (var httpClient = new HttpClient())
            {
                var requestBody = new { amount = sent_amount };
                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await httpClient.PostAsync("http://localhost:5010/api/deposit", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<DepositResponse>(responseBody);
                        return result; // Retornar el mensaje de éxito
                    }
                    else
                    {
                        DepositResponse depResp = new DepositResponse();
                        depResp.Success = false;
                        depResp.Message = "Error al procesar el depósito.";
                        depResp.ModifiedAmount = -1;
                        return depResp;
                    }
                }
                catch(Exception e)
                {
                    DepositResponse depResp = new DepositResponse();
                    depResp.Success = false;
                    depResp.Message = "Error de conexión con el servidor. Excepcion: " + e.Message;
                    depResp.ModifiedAmount = -1;
                    return depResp;
                }
            }
        }
    }

    public class DepositResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public decimal ModifiedAmount { get; set; }

        public override string ToString() => $"Success: {Success}, Message: {Message}, ModifiedAmount: {ModifiedAmount}";
    }
}