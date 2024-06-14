using System.ComponentModel.DataAnnotations.Schema;

namespace GroupExpenses.Domain.Entities
{
   public class Receipt
   {
         public int Id { get; set; }
         public int EventId { get; set; }
         [ForeignKey(nameof(EventId))]
         public Event Event { get; set; }
         public string Name { get; set; }
         public double Price { get; set; }
         public double PriceInEur { get; set; }
         public string Details { get; set; }
         public int Currency { get; set; }
         public int PaidById { get; set; }
         [ForeignKey(nameof(PaidById))]
         public User PaidBy { get; set; }
         public IEnumerable<User> PaidFor { get; set; }
   }
}
