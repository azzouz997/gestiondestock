using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.ProductServices;
using ElamanaTakaful.Application.Services.PropositionServices;
using ElamanaTakaful.Application.Services.SupplierServices;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Application.Services.OrderServices
{
    public class OrderService : IDataRepository<Order>
    {
        readonly ElamanaTakafulContext _context;
        readonly PropositionService _propositionService;

        public OrderService(ElamanaTakafulContext context)
        {
            _context = context;
            _propositionService = new(_context);
        }

        public Order Add(Order order)
        {
            order.CreationStartDate = DateTime.Now;

            User creator = GetUser(order.CreatorId);
            order.Creator = creator;
            Proposition proposition = _propositionService.Get(order.PropositionId);
            order.Proposition = proposition;            
            User validator = GetUser(order.ValidatorId);
            order.Validator = validator;
            order.OrderStatus = "En cours";
            
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public void Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.OrderId == id);
            if (order != null)
            {
                _context.Remove(order);
                _context.SaveChanges();
            }
        }

        public Order Get(int id)
        {
            Order o = _context.Orders.Include(u => u.Proposition).FirstOrDefault(x => x.OrderId == id);
            return o;
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public void Update(Order order)
        {
            Order orderPreviousData = _context.Orders.Include(u => u.Proposition).ThenInclude(p => p.Product).ThenInclude(p => p.Supplier).Include(p => p.OrderHistory).FirstOrDefault(x => x.OrderId == order.OrderId);
            _context.Entry(orderPreviousData).State = EntityState.Detached;

            List<OrderHistory> arr = new();
            arr = orderPreviousData.OrderHistory;

            OrderHistory orderHistory = new();
            orderHistory.OrderId = orderPreviousData.OrderId;
            orderHistory.OrderNumber = orderPreviousData.OrderNumber;
            orderHistory.CreationStartDate = orderPreviousData.CreationStartDate;
            orderHistory.CreationEndDate = orderPreviousData.CreationEndDate;
            orderHistory.ValidationStartDate = orderPreviousData.ValidationStartDate;
            orderHistory.ValidationEndDate = orderPreviousData.ValidationEndDate;
            orderHistory.OrderStatus = orderPreviousData.OrderStatus;
            orderHistory.ValidatorId = orderPreviousData.ValidatorId;
            orderHistory.CreatorId = orderPreviousData.CreatorId;
            orderHistory.PropositionId = orderPreviousData.PropositionId;

            arr.Add(orderHistory);
            order.OrderHistory = arr;

            Proposition proposition = _propositionService.Get(order.PropositionId);
            order.Proposition = proposition;
            if (order.OrderStatus.ToLower().Contains("validée"))
            {
                order.ValidationStartDate = DateTime.Now;
            }
            else if (order.OrderStatus.ToLower().Contains("Livrée"))
            {
                order.ValidationEndDate = DateTime.Now;
            }
            User validator = GetUser(order.ValidatorId);
            order.Validator = validator;
            User creator = GetUser(order.CreatorId);
            order.Creator = creator;

            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        internal User GetUser(int? validatorId)
        {
            var user = _context.Users.Include(u => u.Role).ThenInclude(r => r.Functions).FirstOrDefault(x => x.UserId == validatorId);
            return user;
        }
    }
}
