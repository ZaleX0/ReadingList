using Microsoft.EntityFrameworkCore;
using ReadingList.Data;
using ReadingList.Data.Interfaces;
using ReadingList.Data.Repositories;
using ReadingList.Data.UnitsOfWork;
using ReadingList.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ReadingListDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAutoMapper(typeof(ReadingList.Services.MappingProfiles.ReadingListMappingProfile).Assembly);

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookPriorityRepository, BookPriorityRepository>();
builder.Services.AddScoped<IReadingListUnitOfWork, ReadingListUnitOfWork>();

builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookPriorityService>();
builder.Services.AddScoped<AuthorAndBookSeeder>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build App
var app = builder.Build();

// Seed sample data
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<AuthorAndBookSeeder>();
seeder.Seed();

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
