using GroupExpenses.BLL.DTOs;
using GroupExpenses.Domain.Entities;
using GroupExpenses.DTOs;
using GroupExpenses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupExpenses.BLL.Mappers
{
   public static class Mapper
   {
      public static EventViewModel ToEventViewModel(Event entityEvent)
      {
         return new EventViewModel
         {
            Details = entityEvent.Details,
            Id = entityEvent.Id,
            Location = entityEvent.Location,
            Name = entityEvent.Name,
            Participants = entityEvent.Participants.Select(u => ToUserViewModel(u))
         };
      }

      public static UserViewModel ToUserViewModel(User user)
      {
         return new UserViewModel
         {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            TotalDept = user.TotalDept,
            TotalPaid = user.TotalPaid
         };
      }

      public static Event ToEventEntity(EventViewModel eventViewModel)
      {
         return new Event
         {
            Details = eventViewModel.Details,
            Id = eventViewModel.Id,
            Location = eventViewModel.Location,
            Name = eventViewModel.Name,
            Participants = eventViewModel.Participants.Select(u => ToUserEntity(u))
         };
      }
      public static IEnumerable<EventViewModel> ToEventViewModel(IEnumerable<Event> events)
      {
         return events.Select(e => ToEventViewModel(e));
      }

      public static User ToUserEntity(UserViewModel user)
      {
         return new User
         {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            TotalDept = user.TotalDept,
            TotalPaid = user.TotalPaid
         };
      }

      public static Receipt ToReceiptEntity(ReceiptViewModel receipt)
      {
         return new Receipt
         {
            Id = receipt.Id,
            Currency = (int)receipt.Currency,
            Details = receipt.Description,
            EventId = receipt.EventId,
            Name = receipt.Name,
            PaidById = receipt.PaidBy.Id,
            Price = receipt.Price,
            PriceInEur = receipt.PriceInEur
         };
      }

      public static ReceiptViewModel ToReceiptViewModel(Receipt receipt)
      {
         return new ReceiptViewModel
         {
            Id = receipt.Id,
            Currency = (Currency)receipt.Currency,
            Description = receipt.Details,
            EventId = receipt.EventId,
            Name = receipt.Name,
            PaidBy = ToUserViewModel(receipt.PaidBy),
            Price = receipt.Price,
            PriceInEur = receipt.PriceInEur
         };
      }
   }
}
