using MediatR;
using MilkNest.Application.CQRS.Product.Commands.CreateProduct;
using MilkNest.Application.CQRS.Product.Commands.DeleteProduct;
using MilkNest.Application.CQRS.Product.Commands.UpdateProduct;
using MilkNest.Application.CQRS.Product.Queries.GetProduct;
using MilkNest.Application.CQRS.Product.Queries.GetProducts;
using MilkNest.Application.CQRS.User.Commands.CreateUser;
using MilkNest.Application.CQRS.User.Commands.DeleteUser;
using MilkNest.Application.CQRS.User.Commands.UpdateUser;
using MilkNest.Application.CQRS.User.Queries.GetUser;
using MilkNest.Application.CQRS.User.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.Interfaces
{
    public interface IProductService
    {
        public Task<Guid> CreateProductAsync(CreateProductCommand createProduct);
        public Task<Unit> DeleteProductAsync(DeleteProductCommand deleteProduct);
        public Task<ProductVm> GetProductAsync(GetProductQuery getProduct);
        public Task<Guid> UpdateProductAsync(UpdateProductCommand updateProduct);
        public Task<ProductListVm> GetProductsAsync(GetProductsQuery getProducts);
    }
}
