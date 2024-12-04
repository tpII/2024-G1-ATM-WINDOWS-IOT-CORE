using System;
using System.Threading.Tasks;
using ATM.Core.Services;
using ATM.Core.Enumeratives;
using ATM.Core.Utils;

namespace ATM.Core.UseCases
{
    public class WithdrawUseCase
    {
        MqttService _mqttClientService;
        public WithdrawUseCase(MqttService mqttClientService)
        {
            _mqttClientService = mqttClientService;
        }
        public async Task Execute(int amount)
        {
            if(amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Withdraw amount must be greater than 0");
            }

            string topicString = MqttTopicMapper.GetTopicString(MqttTopic.Withdraw_Request);
            await _mqttClientService.PublishAsync(topicString, amount.ToString());
        }
    }

}