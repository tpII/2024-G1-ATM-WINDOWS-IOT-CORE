using System;
using System.Collections.Generic;
using ATM.Core.Enumeratives;

namespace ATM.Core.Utils 
{
    public static class MqttTopicMapper
    {
        private static readonly Dictionary<MqttTopic, string> TopicMap = new Dictionary<MqttTopic, string>()
        {
            { MqttTopic.CardValidation_Request, "atm/account/card_input" },
            { MqttTopic.PinValidation_Request, "atm/account/pin_input" },
            { MqttTopic.AccountBalance_Request, "atm/account/balance" },
            { MqttTopic.Withdraw_Request, "atm/account/withdraw" },
            { MqttTopic.Deposit_Request, "atm/account/deposit" },
            { MqttTopic.Transfer_Request, "atm/account/transfer" },
            { MqttTopic.Status, "atm/admin/status" },
            { MqttTopic.SetWithdrawalLimits, "atm/admin/set_withdrawal_limits" },
            { MqttTopic.SetCashAmount, "atm/admin/set_cash_amount" },
            { MqttTopic.CardValidation_Response, "atm/system/card_response" },
            { MqttTopic.PinValidation_Response, "atm/system/pin_response" },
            { MqttTopic.AccountBalance_Response, "atm/system/balance_response" },
            { MqttTopic.Withdraw_Response, "atm/system/withdraw_response" },
            { MqttTopic.Deposit_Response, "atm/system/deposit_response" },
            { MqttTopic.Transfer_Response, "atm/system/transfer_response" }
        };

        public static string GetTopicString(MqttTopic topic)
        {
            try
            {
                return TopicMap[topic];
            }
            catch (System.Exception)
            {
                throw new ArgumentOutOfRangeException(nameof(topic), "Topic no válido");
            }          
        }
    }
}