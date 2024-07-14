using GroupExpenses.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupExpenses.BLL.IServices
{
   public interface IReportService
   {
      Task<IEnumerable<DebitKreditByParticipantReportViewModel>> GetDebitsAndKreditsOfUserByOtherUsers(int eventId,int userId);
   }
}
