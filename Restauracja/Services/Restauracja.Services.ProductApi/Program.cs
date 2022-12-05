using Microsoft.EntityFrameworkCore;
using Restauracja.Services.ProductApi.Data;
using Restauracja.Services.ProductApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<DbContext, ApplicationDbContext>();

builder.Services.AddTransient(typeof(IRepository<,>), typeof(BaseRepository<,>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
