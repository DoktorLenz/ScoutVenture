namespace ScoutVenture.CoreContracts.Exceptions
{
    public class ConfirmationCodeException(string publicMessage, string internalMessage)
        : Exception(internalMessage)
    {
        public string PublicMessage { get; init; } = publicMessage;
    }
}