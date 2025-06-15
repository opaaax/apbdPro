namespace APBDProjekt.Tools;

public interface IWalutaProcessor
{
    public Task<decimal> ProcessCurrency(decimal amount, string currency);
}