using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.ViewModels;
using GroupExpenses.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroupExpenses.Controllers
{
   public class ReportController: Controller
   {
      private readonly ILogger<ReportController> _logger;
      private readonly IReportService _reportService;
      public ReportController(ILogger<ReportController> logger,IReportService reportService)
      {
         _logger = logger;
         _reportService = reportService;
      }

      [HttpGet("/kredit-debit/{userId}/{eventId}")]
      public async Task<IActionResult> GetReceiptsByEvent([FromRoute] int userId, int eventId)
      {
         return Ok(await _reportService.GetDebitsAndKreditsOfUserByOtherUsers(eventId, userId));
      }
   }
}
