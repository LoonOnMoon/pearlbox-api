using pearlbox_api.business.Extensions;
using pearlbox_api.web.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

StartupExtensions.ConfigureServices(builder.Services, builder.Environment.EnvironmentName);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

app.MapControllers();

app.Run();
