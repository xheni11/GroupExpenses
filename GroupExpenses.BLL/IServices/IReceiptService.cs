using GroupExpenses.BLL.ViewModels.Receipt;

namespace GroupExpenses.BLL.IServices
{
    public interface IReceiptService
   {
      Task<IEnumerable<GetReceiptViewModel>> GetReceiptsByEventId(int eventId);
      Task<IEnumerable<GetReceiptViewModel>> GetReceiptsPaidBy(int userId);
      Task<IEnumerable<GetReceiptViewModel>> GetReceiptsPaidFor(int userId);
      Task<GetReceiptViewModel> GetById(int id);
      Task<GetReceiptViewModel> Add(AddReceiptViewModel userViewModel);
      Task<GetReceiptViewModel> Update(UpdateReceiptViewModel userViewModel);
      Task Delete(int id);
   }
}
