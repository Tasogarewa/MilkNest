using MilkNest.Application.CQRS.User.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Product.Queries.GetProducts
{
    public class ProductListVm
    {
        public List<ProductDto> ProductDtos { get; set; }
    }
}
