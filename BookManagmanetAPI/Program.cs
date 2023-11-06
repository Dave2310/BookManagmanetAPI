using BookManagmanetAPI;
using BusinessLogic.Services.BookService;
using BusinessLogic.Services.CategoryService;
using BusinessLogic.Services.ReviewerService;
using DataAccess.Data;
using DataAccess.Repositories.BaseRepository;
using DataAccess.Repositories.BookRepository;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Services

builder.Services.AddDbContext<BookContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion 

builder.Services.AddControllers();

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

app.MapControllers();

app.Run();
