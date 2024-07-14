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
      public async Task<IEnumerable<DebitKreditByParticipantReportViewModel>> GetReceiptsByEvent([FromRoute] int userId, int eventId)
      {
         return await _reportService.GetDebitsAndKreditsOfUserByOtherUsers(eventId, userId);
      }
   }
}
