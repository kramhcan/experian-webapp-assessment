using CustomerManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using CustomerManagement.Api.Middleware;
using CustomerManagement.Api.Implementations;
using CustomerManagement.Api.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// register factory in dependency injection container
// scoped lifetime - one instance per request
builder.Services.AddScoped<ICriteriaFactory, CriteriaFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// For Middleware
app.UseMiddleware<ApiKeyMiddleware>(app.Configuration);
app.UseAuthentication();

app.UseHttpsRedirection();

app.MapControllers();
app.Run();