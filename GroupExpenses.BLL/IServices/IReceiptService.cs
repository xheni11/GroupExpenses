
using GroupExpenses.BLL.ViewModels;

namespace GroupExpenses.BLL.IServices
{
   public interface IReceiptService
   {
      Task<IEnumerable<ReceiptViewModel>> GetReceiptsByEventId(int eventId);
      Task<IEnumerable<ReceiptViewModel>> GetReceiptsPaidBy(int userId);
      Task<IEnumerable<ReceiptViewModel>> GetReceiptsPaidFor(int userId);
      Task<ReceiptViewModel> GetById(int id);
      Task<ReceiptViewModel> Add(ReceiptViewModel userViewModel);
      Task<ReceiptViewModel> Update(ReceiptViewModel userViewModel);
      Task Delete(int id);
   }
}
