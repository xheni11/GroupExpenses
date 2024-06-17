using GroupExpenses.Domain.Entities;

namespace GroupExpenses.Domain.IRepositories
{
   public interface IReceiptRepository
   {
      Task<Receipt> GetById(int id);
      Task<IEnumerable<Receipt>> GetReceiptsByEvent(int eventId);
      Task<Receipt>Add(Receipt receipt);
      Task Delete(int id);
      Task Update(Receipt receipt);
      Task<IEnumerable<Receipt>> GetReceiptsPaidForUserId(int userId);
      Task<IEnumerable<Receipt>> GetReceiptsPaidByUserId(int userId);
   }
}
