using GroupExpenses.APIGatway;
using GroupExpenses.BLL.IServices;
using GroupExpenses.BLL.Services;
using GroupExpenses.Config;
using GroupExpenses.Domain.IRepositories;
using GroupExpenses.Domain.Persistence;
using GroupExpenses.Domain.Repositories;
using GroupExpenses.Services;
using GroupExpenses.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ExternalApiSettings>(builder.Configuration.GetSection("ExternalApi"));
builder.Services.AddDbContext<GroupExpensesContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),  x => x.MigrationsAssembly("GroupExpenses.Domain")));

// Register HttpClient
builder.Services.AddHttpClient();

// Register services
builder.Services.AddTransient<ExchangeRateAPIService>();
builder.Services.AddTransient<ReceiptService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IReceiptRepository, ReceiptRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseDeveloperExceptionPage();
   app.UseSwagger();
   app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
