using GroupExpenses.Enums;

namespace GroupExpenses.BLL.IServices
{
   public interface ICurrencyExchangeService
   {
      Task<decimal> ConvertPriceInEur(decimal price,Currency currency);
   }
}
