using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.ViewModels;
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
      public async Task<IEnumerable<ReceiptViewModel>> GetReceiptsByEvent([FromRoute] int eventId)
      {
         return await _receiptService.GetReceiptsByEventId(eventId);
      }

      [HttpGet("/by-paid-by/{paidById}")]
      public async Task<IEnumerable<ReceiptViewModel>> GetReceiptsByPaidBy([FromRoute] int paidById)
      {
         return await _receiptService.GetReceiptsPaidBy(paidById);
      }

      [HttpGet("/by-paid-for/{paidForId}")]
      public async Task<IEnumerable<ReceiptViewModel>> GetReceiptsByPaidFor([FromRoute] int paidForId)
      {
         return await _receiptService.GetReceiptsPaidBy(paidForId);
      }

      [HttpGet("{receiptId}")]
      public async Task<ReceiptViewModel> GetReceipt([FromRoute] int receiptId)
      {
         return await _receiptService.GetById(receiptId);
      }

      [HttpPost]
      public async Task<ReceiptViewModel> Add([FromBody] ReceiptViewModel receipt)
      {
         return await _receiptService.Add(receipt);
      }

      [HttpPut]
      public async Task<ReceiptViewModel> Update([FromBody] ReceiptViewModel receipt)
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
