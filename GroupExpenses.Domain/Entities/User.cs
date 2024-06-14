
namespace GroupExpenses.Domain.Entities
{
   public class User
    {
      public int Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public decimal TotalPaid { get; set; }
      public decimal TotalDept { get; set; }

      public decimal Balance()
      {
         return TotalPaid - TotalDept; 
      }

      public string FullName()
      {
         return $"{FirstName} {LastName}";
      }

    }
}
