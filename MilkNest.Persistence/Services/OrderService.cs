using AutoMapper;
using MediatR;
using MilkNest.Application.Common.Exceptions;
using MilkNest.Application.CQRS.Order.Commands.CreateOrder;
using MilkNest.Application.CQRS.Order.Commands.DeleteOrder;
using MilkNest.Application.CQRS.Order.Queries.GetOrders;
using MilkNest.Application.CQRS.Product.Queries.GetProduct;
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
    public class OrderService : IOrderService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        private readonly ITranslationService _translationService;

        public OrderService(IRepository<User> userRepository, IRepository<Product> productRepository, IRepository<Order> orderRepository, IMapper mapper, ITranslationService translationService)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _translationService = translationService;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderCommand createOrder)
        {
            var Order = await _orderRepository.CreateAsync(new Order() { OrderDate = DateTime.Now, Quantity = createOrder.Quantity, Product = await _productRepository.GetAsync(createOrder.ProductId), User = await _userRepository.GetAsync(createOrder.UserId)});
            return Order.Id;
        }

        public async Task<Unit> DeleteOrderAsync(DeleteOrderCommand deleteOrder)
        {
            var Order = await _orderRepository.GetAsync(deleteOrder.Id);
            if (Order != null)
            {
                await _orderRepository.DeleteAsync(deleteOrder.Id);
                return Unit.Value;
            }
            else
            {
                NotFoundException.Throw(Order, deleteOrder.Id);
                return Unit.Value;
            }
        }

        public async Task<OrderListVm> GetOrdersAsync(GetOrdersQuery getOrders)
        {
            var orders = await _orderRepository.GetAllAsync();

            if (orders != null)
            {
                var orderDtos = _mapper.Map<List<OrderDto>>(orders);

                foreach (var orderDto in orderDtos)
                {
                    orderDto.Language = await _translationService.GetCurrentLanguageAsync();

                    var order = orders.FirstOrDefault(o => o.Id == orderDto.Id);
                    if (order != null)
                    {
                        var productLocalization = order.Product?.Localizations.FirstOrDefault(l => l.Language == orderDto.Language);
                        if (productLocalization != null)
                        {
                            orderDto.ProductName = productLocalization.Title;
                        }
                    }
                }

                return new OrderListVm() { OrderDtos = orderDtos };
            }
            else
            {
                NotFoundException.ThrowRange(orders);
                return null;
            }
        }
    }
}
