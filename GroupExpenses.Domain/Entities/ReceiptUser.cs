using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupExpenses.Domain.Entities
{
   public class ReceiptUser
   {
      public int Id { get; set; }
      public int PaidById { get; set; }
      public int PaidForId { get; set; }
      public int ReceiptId { get; set; }

      [ForeignKey(nameof(PaidForId))]
      public User PaidFor { get; set; }

      [ForeignKey(nameof(PaidById))]
      public User PaidBy { get; set; }
      [ForeignKey(nameof(ReceiptId))]

      public Receipt Receipt { get; set; }
   }
}
