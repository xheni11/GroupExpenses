
namespace GroupExpenses.Domain.Entities
{
   public class User
    {
      public int Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }

      public string FullName()
      {
         return $"{FirstName} {LastName}";
      }

    }
}
