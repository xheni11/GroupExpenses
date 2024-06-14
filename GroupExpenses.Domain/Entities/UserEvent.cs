using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupExpenses.Domain.Entities
{
   public class UserEvent
   {
      public int Id { get; set; }
      public int EventId { get; set; }
      public int ParticipantId { get; set; }

      [ForeignKey(nameof(EventId))]
      public Event Event { get; set; }

      [ForeignKey(nameof(ParticipantId))]
      public User Participant { get; set; }

   }
}
