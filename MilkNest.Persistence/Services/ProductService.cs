﻿using AutoMapper;
using MediatR;
using MilkNest.Application.Common.Exceptions;
using MilkNest.Application.CQRS.Product.Commands.CreateProduct;
using MilkNest.Application.CQRS.Product.Commands.DeleteProduct;
using MilkNest.Application.CQRS.Product.Commands.UpdateProduct;
using MilkNest.Application.CQRS.Product.Queries.GetProduct;
using MilkNest.Application.CQRS.Product.Queries.GetProducts;
using MilkNest.Application.CQRS.User.Queries.GetUser;
using MilkNest.Application.CQRS.User.Queries.GetUsers;
using MilkNest.Application.Interfaces;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Persistence.Services
{
    public class ProductService:IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public ProductService(IRepository<Product> productRepository, IMapper mapper, IFileStorageService fileStorageService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        public async Task<Guid> CreateProductAsync(CreateProductCommand createProduct)
        {
            List<Image> images = new List<Image>();
            foreach (var image in createProduct.Images)
            {
                images.Add(new Image() { Url = await _fileStorageService.SaveFileAsync(image)});
            }
            var Product = await _productRepository.CreateAsync(new Product() { Amount = createProduct.Amount, Description = createProduct.Description, Name= createProduct.Name, Price = createProduct.Price, Images = images });
            return Product.Id;
        }

        public async Task<Unit> DeleteProductAsync(DeleteProductCommand deleteProduct)
        {
            var Product = await _productRepository.GetAsync(deleteProduct.Id);
            if (Product != null)
            {
                await _productRepository.DeleteAsync(deleteProduct.Id);
                return Unit.Value;
            }
            else
            {
                NotFoundException.Throw(Product, deleteProduct.Id);
                return Unit.Value;
            }
        }

        public async Task<ProductVm> GetProductAsync(GetProductQuery getProduct)
        {
            var Product = await _productRepository.GetAsync(getProduct.Id);
            if (Product != null)
            {
                return _mapper.Map<ProductVm>(Product);
            }
            else
            {
                NotFoundException.Throw(Product, getProduct.Id);
                return null;
            }
        }

        public async Task<ProductListVm> GetProductsAsync(GetProductsQuery getProducts)
        {
            var Products = await _productRepository.GetAllAsync();
            if (Products != null)
            {
                var productDtos = _mapper.ProjectTo<ProductDto>(Products.AsQueryable()).ToList();
                return new ProductListVm() { ProductDtos = productDtos };
            }
            else
            {
                NotFoundException.ThrowRange(Products);
                return null;
            }
        }

        public async Task<Guid> UpdateProductAsync(UpdateProductCommand updateProduct)
        {
            var Product = await _productRepository.GetAsync(updateProduct.Id);
            List<Image> NewPhotos = new List<Image>();
            if (Product != null)
            {
              foreach(var img in Product.Images)
                {
                    _fileStorageService.DeleteFile(img.Url);
                }
                foreach (var img in updateProduct.Images)
                {
                    Product.Images.Add(new Image() { Url = await _fileStorageService.SaveFileAsync(img) });
                }
                Product.Amount = updateProduct.Amount;
                Product.Name = updateProduct.Name;
                Product.Price = updateProduct.Price;
                Product.Description = updateProduct.Description;
                await _productRepository.UpdateAsync(Product);
                return Product.Id;
            }
            else
            {
                NotFoundException.Throw(Product, updateProduct.Id);
                return Guid.Empty;
            }
        }
    }
}
