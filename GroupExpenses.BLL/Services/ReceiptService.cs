using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.Mappers;
using GroupExpenses.BLL.ViewModels.Receipt;
using GroupExpenses.Domain.IRepositories;

namespace GroupExpenses.Services
{
    public class ReceiptService : IReceiptService
   {
      private readonly IReceiptRepository _receiptRepository;
      private readonly ICurrencyExchangeService _currencyExchangeService;
      public ReceiptService(IReceiptRepository receiptRepository)
      {
         _receiptRepository = receiptRepository;
      }
      public async Task<IEnumerable<GetReceiptViewModel>> GetReceiptsByEventId(int eventId)
      {
         var receipts = await _receiptRepository.GetReceiptsByEvent(eventId);
         return ReceiptMapper.ToViewModel(receipts);
      }
      public async Task<IEnumerable<GetReceiptViewModel>> GetReceiptsPaidBy(int userId)
      {
         var receipts = await _receiptRepository.GetReceiptsPaidByUserId(userId);
         return ReceiptMapper.ToViewModel(receipts);
      }
      public async Task<IEnumerable<GetReceiptViewModel>> GetReceiptsPaidFor(int userId)
      {
         var receipts = await _receiptRepository.GetReceiptsPaidForUserId(userId);
         return ReceiptMapper.ToViewModel(receipts);
      }
      public async Task<GetReceiptViewModel> GetById(int id)
      {
         var receipt = await _receiptRepository.GetById(id);
         return ReceiptMapper.ToViewModel(receipt);
      }
      public async Task<GetReceiptViewModel> Update(UpdateReceiptViewModel receipt)
      {
         var priceInEur = await _currencyExchangeService.ConvertPriceInEur(receipt.Price, receipt.Currency);
         await _receiptRepository.Update(ReceiptMapper.ToEntity(receipt, priceInEur));
         return await GetById(receipt.Id);
      }
      public async Task<GetReceiptViewModel> Add(AddReceiptViewModel receipt)
      {
         var priceInEur = await _currencyExchangeService.ConvertPriceInEur(receipt.Price,receipt.Currency);
         var addedReceipt = await _receiptRepository.Add(ReceiptMapper.ToEntity(receipt, priceInEur));
         return ReceiptMapper.ToViewModel(addedReceipt);
      }
      public async Task Delete(int id)
      {
         await _receiptRepository.Delete(id);
      }

   }
}
