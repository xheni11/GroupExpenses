using GroupExpenses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GroupExpenses.Domain.Persistence
{
   public class GroupExpensesContext:DbContext
   {
        public GroupExpensesContext(DbContextOptions<GroupExpensesContext> options) : base(options)
         {

         }

      public DbSet<User> Users { get; set; }
      public DbSet<Receipt> Receipts { get; set; }
      public DbSet<Event> Events { get; set; }
      public DbSet<UserEvent> UserEvents { get; set; }
      public DbSet<ReceiptUser> ReceiptUsers { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Event>().ToTable("Event");
         modelBuilder.Entity<User>().ToTable("User");
         modelBuilder.Entity<UserEvent>().ToTable("UserEvent");
         modelBuilder.Entity<Receipt>().ToTable("Receipt");
         modelBuilder.Entity<ReceiptUser>().ToTable("ReceiptUser");
         ;
      }
   }
}
