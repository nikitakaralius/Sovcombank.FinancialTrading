using Sovcombank.FinancialTrading.Application;
using Sovcombank.FinancialTrading.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(options =>
{
    options.EsConnectionString = builder.Configuration.GetConnectionString("EventStore")!;
    options.EsConnectionName = builder.Environment.ApplicationName;
    options.PostgresConnectionString = builder.Configuration.GetConnectionString("Postgres")!;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

app.Run();
