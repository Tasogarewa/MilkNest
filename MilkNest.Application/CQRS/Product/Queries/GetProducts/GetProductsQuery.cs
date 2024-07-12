﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Product.Queries.GetProducts
{
    public class GetProductsQuery:IRequest<ProductListVm>
    {
    }
}
