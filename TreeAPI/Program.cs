using DataAccess;
using Microsoft.EntityFrameworkCore;
using TreeAPI.Services;
using TreeAPI.Services.IRepository;
using TreeAPI.Services.IRepository.Repository;
using TreeAPI.Services.TreeAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register IConfiguration and bind the appsettings.json file
builder.Services.AddDbContext<TreeApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IJournalRepository, JournalRepository>();
builder.Services.AddScoped<JournalService>();

builder.Services.AddScoped<INodeRepository, NodeRepository>();
builder.Services.AddScoped<NodeService>();

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