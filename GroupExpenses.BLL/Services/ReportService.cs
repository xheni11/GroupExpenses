using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.ViewModels;
using GroupExpenses.Domain.IRepositories;

namespace GroupExpenses.BLL.Services
{
   public class ReportService : IReportService
   {
      private readonly IReceiptRepository _receiptRepository;
      public ReportService(IReceiptRepository receiptRepository)
      {
         _receiptRepository = receiptRepository;
      }

      public async Task<IEnumerable<DebitKreditByParticipantReportViewModel>> GetDebitsAndKreditsOfUserByOtherUsers(int eventId,int userId)
      {
         var receipts = await _receiptRepository.GetReceiptsByEvent(eventId);

         var debits = receipts
            .Where(r => r.PaidBy.Id == userId)
            .GroupBy(r => r.PaidFor)
            .Select(r => new DebitKreditByParticipantReportViewModel
               {
                  Debit = r.Sum(x => x.Price),
                  ParticipantName = r.Key.First().FullName
               })
            .ToList();

         var kredits = receipts
            .Where(r => r.PaidFor.Select(u => u.Id).Contains(userId))
            .GroupBy(r => r.PaidBy)
            .Select(r => new DebitKreditByParticipantReportViewModel
            {
               Kredit = r.Sum(x => x.Price),
               ParticipantName = r.Key.FullName
            })
            .ToList();

         return debits
            .Union(kredits)
            .GroupBy(r => r.ParticipantName)
            .Select(r => new DebitKreditByParticipantReportViewModel
             {
                Kredit = r.Sum(k => k.Kredit),
                Debit = r.Sum(k => k.Debit),
                ParticipantName = r.Key
             });
      }
   }
}
