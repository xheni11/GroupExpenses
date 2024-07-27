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
      public async Task<IActionResult> GetReceiptsByEvent([FromRoute] int eventId)
      {
         return Ok(await _receiptService.GetReceiptsByEventId(eventId));
      }

      [HttpGet("/by-paid-by/{paidById}")]
      public async Task<IActionResult> GetReceiptsByPaidBy([FromRoute] int paidById)
      {
         return Ok(await _receiptService.GetReceiptsPaidBy(paidById));
      }

      [HttpGet("/by-paid-for/{paidForId}")]
      public async Task<IActionResult> GetReceiptsByPaidFor([FromRoute] int paidForId)
      {
         return Ok(await _receiptService.GetReceiptsPaidBy(paidForId));
      }

      [HttpGet("{receiptId}")]
      public async Task<IActionResult> GetReceipt([FromRoute] int receiptId)
      {
         return Ok(await _receiptService.GetById(receiptId));
      }

      [HttpPost]
      public async Task<IActionResult> Add([FromBody] AddReceiptViewModel receipt)
      {
         return Ok(await _receiptService.Add(receipt));
      }

      [HttpPut]
      public async Task<GetReceiptViewModel> Update([FromBody] UpdateReceiptViewModel receipt)
      {
         return await _receiptService.Update(receipt);
      }

      [HttpDelete("{receptId}")]
      public async Task<IActionResult> Delete([FromRoute] int receptId)
      {
         await _receiptService.Delete(receptId);
         return Ok() ;
      }
   }
}
