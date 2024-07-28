using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.Services;
using GroupExpenses.Domain.Entities;
using GroupExpenses.Domain.IRepositories;
using Moq;

namespace GroupExpenses.UnitTests
{
   public class ReportService_Tests
   {
      private readonly Mock<IReceiptRepository> _receiptRepositoryMock;
      private readonly IReportService _reportService;

      public ReportService_Tests()
      {
         _receiptRepositoryMock = new Mock<IReceiptRepository>();
         _reportService = new ReportService(_receiptRepositoryMock.Object);
      }

      [Fact]
      public async Task GetDebitsAndKreditsOfUserByOtherUsers_UserHasDebitsAndKredits_ReturnsCorrectReport()
      {
         // Arrange
         int eventId = 1;
         int userId = 1;
         var receipts = new List<Receipt>
        {
            new Receipt
            {
                PaidBy = new User { Id = userId, FirstName = "User", LastName ="A"},
                PaidFor = new List<User> { new User { Id = 2,FirstName = "User",LastName = "B" } },
                Price = 100,
                EventId = eventId,
            },
            new Receipt
            {
                PaidBy = new User { Id = 2,FirstName = "User", LastName = "B" },
                PaidFor = new List<User> { new User { Id = userId, FirstName = "User", LastName = "A" } },
                Price = 50,
                EventId = eventId
            }
        };

         _receiptRepositoryMock
             .Setup(repo => repo.GetReceiptsByEvent(eventId))
             .ReturnsAsync(receipts);

         // Act
         var result = await _reportService.GetDebitsAndKreditsOfUserByOtherUsers(eventId,userId);

         // Assert
         var resultList = result.ToList();
         Assert.Single(resultList);

         var debitEntry = resultList.First(r => r.ParticipantName == "User B");
         Assert.Equal(100,debitEntry.Debit);
         Assert.Equal(50,debitEntry.Kredit);
      }

      [Fact]
      public async Task GetDebitsAndKreditsOfUserByOtherUsers_UserHasNoDebitsOrKredits_ReturnsEmptyReport()
      {
         // Arrange
         int eventId = 1;
         int userId = 1;
         var receipts = new List<Receipt>();

         _receiptRepositoryMock
             .Setup(repo => repo.GetReceiptsByEvent(eventId))
             .ReturnsAsync(receipts);

         // Act
         var result = await _reportService.GetDebitsAndKreditsOfUserByOtherUsers(eventId,userId);

         // Assert
         Assert.Empty(result);
      }

      [Fact]
      public async Task GetDebitsAndKreditsOfUserByOtherUsers_UserHasOnlyDebits_ReturnsOnlyDebits()
      {
         // Arrange
         int eventId = 1;
         int userId = 1;
         var receipts = new List<Receipt>
        {
            new Receipt
            {
                PaidBy = new User { Id = userId,FirstName = "User", LastName ="A" },
                PaidFor = new List<User> { new User { Id = 2,FirstName = "User",LastName = "B" } },
                Price = 100
            }
        };

         _receiptRepositoryMock
             .Setup(repo => repo.GetReceiptsByEvent(eventId))
             .ReturnsAsync(receipts);

         // Act
         var result = await _reportService.GetDebitsAndKreditsOfUserByOtherUsers(eventId,userId);

         // Assert
         var resultList = result.ToList();
         Assert.Single(resultList);

         var debitEntry = resultList.First();
         Assert.Equal(100,debitEntry.Debit);
         Assert.Equal(0,debitEntry.Kredit);
         Assert.Equal("User B",debitEntry.ParticipantName);
      }

      [Fact]
      public async Task GetDebitsAndKreditsOfUserByOtherUsers_UserHasOnlyKredits_ReturnsOnlyKredits()
      {
         // Arrange
         int eventId = 1;
         int userId = 1;
         var receipts = new List<Receipt>
        {
            new Receipt
            {
                PaidBy = new User { Id = 2, FirstName = "User", LastName ="B" },
                PaidFor = new List<User> { new User { Id = userId,FirstName = "User", LastName = "A" } },
                Price = 50
            }
        };

         _receiptRepositoryMock
             .Setup(repo => repo.GetReceiptsByEvent(eventId))
             .ReturnsAsync(receipts);

         // Act
         var result = await _reportService.GetDebitsAndKreditsOfUserByOtherUsers(eventId,userId);

         // Assert
         var resultList = result.ToList();
         Assert.Single(resultList);

         var kreditEntry = resultList.First();
         Assert.Equal(50,kreditEntry.Kredit);
         Assert.Equal(0,kreditEntry.Debit);
         Assert.Equal("User B",kreditEntry.ParticipantName);
      }
   }
}