using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupExpenses.BLL.ViewModels
{
   public record DebitKreditByParticipantReportViewModel
   {
      public decimal Debit { get; set; } 
      public decimal Kredit { get; set; }
      public decimal Total { get => Debit - Kredit; }

      [Display(Name = "Participant Name")]
      public string ParticipantName { get; set; }

   }
}
