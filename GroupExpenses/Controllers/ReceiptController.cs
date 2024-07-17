using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.ViewModels.Receipt;
using Microsoft.AspNetCore.Mvc;

namespace GroupExpenses.Controllers
{
    [ApiController]
   [Route("[controller]")]
   public class ReceiptController: ControllerBase
   {

      private readonly ILogger<ReceiptController> _logger;
      private readonly IReceiptService _receiptService;
      public ReceiptController(ILogger<ReceiptController> logger,IReceiptService receiptService)
      {
         _logger = logger;
         _receiptService = receiptService;
      }

      [HttpGet("/by-event/{eventId}")]
      public async Task<IEnumerable<GetReceiptViewModel>> GetReceiptsByEvent([FromRoute] int eventId)
      {
         return await _receiptService.GetReceiptsByEventId(eventId);
      }

      [HttpGet("/by-paid-by/{paidById}")]
      public async Task<IEnumerable<GetReceiptViewModel>> GetReceiptsByPaidBy([FromRoute] int paidById)
      {
         return await _receiptService.GetReceiptsPaidBy(paidById);
      }

      [HttpGet("/by-paid-for/{paidForId}")]
      public async Task<IEnumerable<GetReceiptViewModel>> GetReceiptsByPaidFor([FromRoute] int paidForId)
      {
         return await _receiptService.GetReceiptsPaidBy(paidForId);
      }

      [HttpGet("{receiptId}")]
      public async Task<GetReceiptViewModel> GetReceipt([FromRoute] int receiptId)
      {
         return await _receiptService.GetById(receiptId);
      }

      [HttpPost]
      public async Task<GetReceiptViewModel> Add([FromBody] GetReceiptViewModel receipt)
      {
         return await _receiptService.Add(receipt);
      }

      [HttpPut]
      public async Task<GetReceiptViewModel> Update([FromBody] GetReceiptViewModel receipt)
      {
         return await _receiptService.Update(receipt);
      }

      [HttpDelete("{receptId}")]
      public async Task<bool> Delete([FromRoute] int receptId)
      {
         await _receiptService.Delete(receptId);
         // TODO add a response view model 
         return true ;
      }
   }
}
