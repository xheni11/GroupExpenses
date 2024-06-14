

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GroupExpenses.Domain.Entities
{
   public class Event
   {
      [NotNull]
      public int Id { get; set; }
      [NotNull]
      [MaxLength(100)]
      public string Name { get; set; }
      [MaxLength(500)]
      public string Details { get; set; }

      [MaxLength(200)]
      public string? Location { get; set; }
      public IEnumerable<User> Participants { get; set; }
   }
}
