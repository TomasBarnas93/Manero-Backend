﻿using Manero_Backend.Contexts;
using Manero_Backend.Helpers;
using Manero_Backend.Helpers.Repositories;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Schemas.Product;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
	{
		private readonly ManeroDbContext _context;

		public ProductRepository(ManeroDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<List<ProductEntity>> GetByOption(ProductOptionSchema option)
		{



			return await _context.Products
				.Include(x => x.TagProducts).ThenInclude(x => x.Tag)
				.Include(x => x.Category)
				.Include(x => x.ProductColors).ThenInclude(x => x.Color)
				.Include(x => x.ProductSizes).ThenInclude(x => x.Size)
				.Include(x => x.Reviews)
				.Where(x => (x.Category.Id == option.CategoryId && x.TagProducts.Any(a => a.Tag.Id == option.TagId)) || (x.CategoryId == option.CategoryId) || (x.TagProducts.Any(a => a.Tag.Id == option.TagId))).Take(option.Count).ToListAsync();
				//.Where(x => x.Category.Id == option.CategoryId && x.TagProducts.Any(a => a.Tag.Id == option.TagId)).Take(option.Count).ToListAsync();

        }





        public async Task FillDataAsync()
		{
            List<TagEntity> tagEntities = new List<TagEntity>()
            {
                //new TagEntity() { Name = "Featured" },
				new TagEntity() { Name = "Popular"},
				new TagEntity() { Name = "Best"},
				new TagEntity() { Name = "New"},
				new TagEntity() { Name = "Men"},
				new TagEntity() { Name = "Women"},
				new TagEntity() { Name = "Kid"}
            };

			List<CategoryEntity> categoryEntities = new List<CategoryEntity>()
			{
				new CategoryEntity() { Name = "Pants"},
				new CategoryEntity() { Name = "Jackets"},
				new CategoryEntity() { Name = "Shoes"},
				new CategoryEntity() { Name = "Dresses"},
				new CategoryEntity() { Name = "T-shirts"},
				new CategoryEntity() { Name = "Accessories"},
			};

			List<OrderStatusTypeEntity> orderStatusTypeEntities = new List<OrderStatusTypeEntity>()
			{ 
				new OrderStatusTypeEntity() { Name = "Order Created" },
				new OrderStatusTypeEntity() { Name = "Order Confirmed" },
				new OrderStatusTypeEntity() { Name = "Order Shipping" },
				new OrderStatusTypeEntity() { Name = "Order Delivering" },
				new OrderStatusTypeEntity() { Name = "Receiving" },
				new OrderStatusTypeEntity() { Name = "Cancelled" }
			};

            List<ColorEntity> colorEntities = new List<ColorEntity>()
            {
                new ColorEntity() { Name = "Red", Hex = "#fd3412" },
				new ColorEntity() { Name = "Blue", Hex = "#21a5ff" },
				new ColorEntity() { Name = "Beige", Hex = "#fff8e7'" },
				new ColorEntity() { Name = "Dark Blue", Hex = "#140062" },
				new ColorEntity() { Name = "Black", Hex = "#000000" },
            };

            List<SizeEntity> sizeEntities = new List<SizeEntity>()
            {
                new SizeEntity() { Name = "XS" },
				new SizeEntity() { Name = "S" },
				new SizeEntity() { Name = "M" },
				new SizeEntity() { Name = "L" },
				new SizeEntity() { Name = "XL" },
				new SizeEntity() { Name = "XXL" }
            };

            List<CompanyEntity> companyEntities = new List<CompanyEntity>()
            {
                new CompanyEntity() { Name = "Acme Co." },
				new CompanyEntity() { Name = "Abstergo Ltd." },
				new CompanyEntity() { Name = "Barone LLC." },
				new CompanyEntity() { Name = "Gucci Ltd." }
            };

            await _context.AddRangeAsync(tagEntities);
			await _context.AddRangeAsync(categoryEntities);
			await _context.AddRangeAsync(orderStatusTypeEntities);
			await _context.AddRangeAsync(colorEntities);
			await _context.AddRangeAsync(sizeEntities);
			await _context.AddRangeAsync(companyEntities);

            await _context.SaveChangesAsync();
        }
	}
}
