using GroupExpenses.Domain.Entities;
using GroupExpenses.Domain.IRepositories;
using GroupExpenses.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GroupExpenses.Domain.Repositories
{
   public class ReceiptRepository:IReceiptRepository
   {
        private readonly DbSet<Receipt> _receipt;
        private readonly DbSet<ReceiptUser> _receiptUser;
        private readonly GroupExpensesContext _context;
        public ReceiptRepository(GroupExpensesContext context)
        {
            _context = context;
            _receipt = context.Set<Receipt>();
            _receiptUser = context.Set<ReceiptUser>();
        }
        public async Task<Receipt> GetById(int id)
        {   
            return await _receipt.FindAsync(id);
        }
      public async Task<IEnumerable<Receipt>> GetReceiptsByEvent(int eventId)
      {
         return await _receipt.Where(r => r.EventId == eventId).ToListAsync();
      }
      public async Task<IEnumerable<Receipt>> GetReceiptsPaidForUserId(int userId)
      {
         return await _receiptUser.Where(r => r.PaidForId == userId).Select(r => r.Receipt).ToListAsync();
      }
      public async Task<IEnumerable<Receipt>> GetReceiptsPaidByUserId(int userId)
      {
         return await _receiptUser.Where(r => r.PaidById == userId).Select(r => r.Receipt).ToListAsync();
      }
      public async Task<Receipt> Add(Receipt receipt)
      {
         var addedReceipt = await _receipt.AddAsync(receipt);
         await  _context.SaveChangesAsync();
         return addedReceipt.Entity;
      }
     
      public async Task Delete(int id)
      {
         var receiptToDelete = await _receipt.FindAsync(id);
         if (receiptToDelete != null)
         {
            _receipt.Remove(receiptToDelete);
         }
         await _context.SaveChangesAsync();
      }

      public async Task Update(Receipt receipt)
      {
         _receipt.Attach(receipt);
         _context.Entry(receipt).State = EntityState.Modified;
         await _context.SaveChangesAsync();
      }
   }
}
