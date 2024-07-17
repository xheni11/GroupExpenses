using GroupExpenses.BLL.ViewModels.Event;
using GroupExpenses.BLL.ViewModels.User;
using GroupExpenses.Domain.Entities;

namespace GroupExpenses.BLL.Mappers
{
    public static class UserMapper
   {
      public static GetUserViewModel ToViewModel(User user)
      {
         return new GetUserViewModel
         {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName
         };
      }

      public static User ToEntity(AddUserViewModel user)
      {
         return new User
         {
            FirstName = user.FirstName,
            LastName = user.LastName
         };
      }

      public static User ToEntity(UpdateUserViewModel user)
      {
         return new User
         {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName
         };
      }
   }
}
