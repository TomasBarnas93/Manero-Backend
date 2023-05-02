using Manero_Backend.Contexts;
using Manero_Backend.Helpers.Repositories;
using Manero_Backend.Helpers.Services;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;
using Manero_Backend.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Contexts
//builder.Services.AddDbContext<ManeroDbContext>
//    (x => x.UseSqlServer(builder.Configuration.GetConnectionString("ManeroStoreDB")));

//builder.Services.AddDbContext<ManeroDbContext>
//    (x => x.UseSqlServer(builder.Configuration.GetConnectionString("ManeroIdentityDB")));

builder.Services.AddDbContext<ManeroDbContext>
	(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SirojsSql")))


builder.Services.AddDbContext<ManeroDbContext>
    (x => x.UseSqlServer(builder.Configuration.GetConnectionString("AdisSql")));

//Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();


//Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReviewService, ReviewService>();






var app = builder.Build();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();