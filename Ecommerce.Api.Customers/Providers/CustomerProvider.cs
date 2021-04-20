using AutoMapper;
using Ecommerce.Api.Customers.Interfaces;
using Ecommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomerProvider:ICustomerProvider
    {

        private readonly CustomerDbContext dbContext;
        private readonly ILogger<CustomerProvider> logger;
        private readonly IMapper mapper;

        public CustomerProvider(CustomerDbContext dbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.dbContext = dbContext;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new DB.Customer() { Id = 1, Name = "Abraham",Address="Address1" });
                dbContext.Customers.Add(new DB.Customer() { Id = 2, Name = "Sruthy",  Address = "Address2" });
                dbContext.Customers.Add(new DB.Customer() { Id = 3, Name = "Akshay", Address = "Address3" });
                dbContext.Customers.Add(new DB.Customer() { Id = 4, Name = "Tarzan", Address = "Address4" });
                dbContext.SaveChanges();
            }

        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customer, string ErrorMessage)> GetCustomerAsync()
        {
            try
            {
                var products = await dbContext.Customers.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Customer>, IEnumerable<Models.Customer>>(products);
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

        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var product = await dbContext.Customers.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<DB.Customer, Models.Customer>(product);
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
