namespace ATM.Core.Enumeratives
{
    public enum MqttTopic
    {
        CardValidation_Request,
        PinValidation_Request,
        AccountBalance_Request,
        Deposit_Request,
        Withdraw_Request,
        Transfer_Request,
        Status, 
        SetWithdrawalLimits,
        SetCashAmount,
        CardValidation_Response,
        PinValidation_Response,
        AccountBalance_Response,
        Deposit_Response,
        Withdraw_Response,
        Transfer_Response
    }
}