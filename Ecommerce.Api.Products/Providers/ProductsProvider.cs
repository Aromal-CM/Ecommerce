using AutoMapper;
using Ecommerce.Api.Products.DB;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext,ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.dbContext = dbContext;

            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Products.Any())
            {
                dbContext.Products.Add(new DB.Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new DB.Product() { Id = 2, Name = "Mouse", Price = 5, Inventory = 200 });
                dbContext.Products.Add(new DB.Product() { Id = 3, Name = "Monitor", Price = 100, Inventory = 300 });
                dbContext.Products.Add(new DB.Product() { Id = 4, Name = "CPU", Price = 250, Inventory = 400 });
                dbContext.SaveChanges();
            }

        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Product, string ErrorMessage)> GetProductAsync()
        {
           try
            {
                var products = await dbContext.Products.ToListAsync();
                if(products!=null && products.Any())
                {
                    var result =mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p=>p.Id==id);
                if (product != null)
                {
                    var result = mapper.Map<DB.Product, Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
