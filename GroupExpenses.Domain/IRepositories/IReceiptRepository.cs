using GroupExpenses.Domain.Entities;

namespace GroupExpenses.Domain.IRepositories
{
   public interface IReceiptRepository
   {
      Task<Receipt> GetById(int id);
      Task<IEnumerable<Receipt>> GetReceiptsByEvent(int eventId);
      Task<int>Add(Receipt receipt);
      Task Delete(int id);
      Task Update(Receipt receipt);
   }
}
