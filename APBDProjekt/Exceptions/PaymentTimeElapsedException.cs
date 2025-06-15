namespace APBDProjekt.Exceptions;

public class PaymentTimeElapsedException : Exception
{
    public PaymentTimeElapsedException()
    {
    }

    public PaymentTimeElapsedException(string? message) : base(message)
    {
    }

    public PaymentTimeElapsedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}