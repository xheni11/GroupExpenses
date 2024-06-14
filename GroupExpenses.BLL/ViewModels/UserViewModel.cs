namespace GroupExpenses.DTOs
{
   public class UserViewModel
   {
      public int Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public decimal TotalPaid { get; set; }
      public decimal TotalDept { get; set; }
   }
}
