using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Profiles
{
    public class OrderProfile:AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<DB.Order, Models.Order>();
            CreateMap<DB.OrderItem, Models.OrderItem>();

        }
    }
}
