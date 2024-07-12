using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Product.Queries.GetProduct
{
    public class GetProductQuery:IRequest<ProductVm>
    {
        public Guid Id { get; set; }
    }
}
