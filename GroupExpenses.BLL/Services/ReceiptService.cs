using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.Mappers;
using GroupExpenses.BLL.ViewModels;
using GroupExpenses.Domain.IRepositories;

namespace GroupExpenses.Services
{
   public class ReceiptService : IReceiptService
   {
      private readonly IReceiptRepository _receiptRepository;
      public ReceiptService(IReceiptRepository receiptRepository)
      {
         _receiptRepository = receiptRepository;
      }
      public async Task<IEnumerable<ReceiptViewModel>> GetReceiptsByEventId(int eventId)
      {
         var receipts = await _receiptRepository.GetReceiptsByEvent(eventId);
         return Mapper.ToReceiptViewModel(receipts);
      }
      public async Task<IEnumerable<ReceiptViewModel>> GetReceiptsPaidBy(int userId)
      {
         var receipts = await _receiptRepository.GetReceiptsPaidByUserId(userId);
         return Mapper.ToReceiptViewModel(receipts);
      }
      public async Task<IEnumerable<ReceiptViewModel>> GetReceiptsPaidFor(int userId)
      {
         var receipts = await _receiptRepository.GetReceiptsPaidForUserId(userId);
         return Mapper.ToReceiptViewModel(receipts);
      }
      public async Task<ReceiptViewModel> GetById(int id)
      {
         var receipt = await _receiptRepository.GetById(id);
         return Mapper.ToReceiptViewModel(receipt);
      }
      public async Task<ReceiptViewModel> Update(ReceiptViewModel receipt)
      {
         await _receiptRepository.Update(Mapper.ToReceiptEntity(receipt));
         return await GetById(receipt.Id);
      }
      public async Task<ReceiptViewModel> Add(ReceiptViewModel receipt)
      {
         var addedReceipt = await _receiptRepository.Add(Mapper.ToReceiptEntity(receipt));
         return Mapper.ToReceiptViewModel(addedReceipt);
      }
      public async Task Delete(int id)
      {
         await _receiptRepository.Delete(id);
      }

   }
}
