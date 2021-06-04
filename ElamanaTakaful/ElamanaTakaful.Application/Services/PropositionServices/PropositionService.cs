using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.SupplierServices;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElamanaTakaful.Application.Services.ProductServices;
using ElamanaTakaful.Application.Services.UserServices;
using ElamanaTakaful.Application.Services.RoleServices;
using ElamanaTakaful.Application.Services.FunctionServices;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Bson;
using System.IO;

namespace ElamanaTakaful.Application.Services.PropositionServices
{
    public class PropositionService : IDataRepository<Proposition>
    {
        readonly ElamanaTakafulContext _context;
        readonly SupplierService _supplierService;
        readonly ProductService _productService;

        public PropositionService(ElamanaTakafulContext context)
        {
            _context = context;
            _supplierService = new(_context);
            _productService = new(_context);
        }

        public Proposition Add(Proposition proposition)
        {
            proposition.StartDate = DateTime.Now;

            Supplier supplier = _supplierService.Get(proposition.SupplierId);
            proposition.Supplier = supplier;
            Product product = _productService.Get(proposition.ProductId);
            proposition.Product = product;

            //Referring a null validator
            User validator = GetUser(proposition.ValidatorId);
            proposition.Validator = validator;

            _context.Propositions.Add(proposition);
            _context.SaveChanges();
            return proposition;
        }

        public void Delete(int id)
        {
            var proposition = _context.Propositions.FirstOrDefault(x => x.PropositionId == id);
            if (proposition != null)
            {
                _context.Remove(proposition);
                _context.SaveChanges();
            }
        }

        public Proposition Get(int id)
        {
            return _context.Propositions.Include(p => p.PropositionHistory).FirstOrDefault(x => x.PropositionId == id);
        }

        public List<Proposition> GetAll()
        {
            return _context.Propositions.ToList();
        }

        public void Update(Proposition proposition)
        {
            Proposition propositionPreviousData = _context.Propositions.Include(u => u.PropositionHistory).Include(p => p.QuoteHistory).FirstOrDefault(x => x.PropositionId == proposition.PropositionId);
            _context.Entry(propositionPreviousData).State = EntityState.Detached;

            List<PropositionHistory> arr = propositionPreviousData.PropositionHistory;
            List<QuoteHistory> arr2 = propositionPreviousData.QuoteHistory;


            if(arr2.Count >= 1) 
            { 
                if(arr2[arr2.Count-1].QuoteFileId != proposition.QuoteId)
                {
                    QuoteHistory quoteHistory = new();
                    quoteHistory.QuoteFileId = propositionPreviousData.QuoteId;
                    quoteHistory.QuoteFileName = propositionPreviousData.QuoteName;
                    quoteHistory.UptadeDate = DateTime.Now;
                    arr2.Add(quoteHistory);
                    proposition.QuoteHistory = arr2;
                }
            }
            else
            {
                QuoteHistory quoteHistory = new();
                quoteHistory.QuoteFileId = propositionPreviousData.QuoteId;
                quoteHistory.QuoteFileName = propositionPreviousData.QuoteName;
                quoteHistory.UptadeDate = DateTime.Now;
                arr2.Add(quoteHistory);
                proposition.QuoteHistory = arr2;
            }

            PropositionHistory propositionHistory = new();
            propositionHistory.PropositionId = propositionPreviousData.PropositionId;
            propositionHistory.PropositionNumber = propositionPreviousData.PropositionNumber;
            propositionHistory.StartDate = propositionPreviousData.StartDate;
            propositionHistory.EndDate = propositionPreviousData.EndDate;
            propositionHistory.PropositionStatus = propositionPreviousData.PropositionStatus;
            propositionHistory.ValidationDate = propositionPreviousData.ValidationDate;
            propositionHistory.AmountTTC = propositionPreviousData.AmountTTC;
            propositionHistory.AmountHT = propositionPreviousData.AmountHT;
            propositionHistory.Direction = propositionPreviousData.Direction;
            propositionHistory.Quantity = propositionPreviousData.Quantity;
            propositionHistory.QuoteId = propositionPreviousData.QuoteId;
            propositionHistory.QuoteName = propositionPreviousData.QuoteName;
            propositionHistory.ValidatorId = propositionPreviousData.ValidatorId;
            propositionHistory.ProductId = propositionPreviousData.ProductId;
            propositionHistory.SupplierId = propositionPreviousData.SupplierId;

            arr.Add(propositionHistory);
            proposition.PropositionHistory = arr;

            Supplier supplier = _supplierService.Get(proposition.SupplierId);
            proposition.Supplier = supplier;
            Product product = _productService.Get(proposition.ProductId);
            proposition.Product = product;
            User validator = GetUser(proposition.ValidatorId);
            proposition.Validator = validator;

            _context.Propositions.Update(proposition);
            _context.SaveChanges();
        }

        internal User GetUser(int? validatorId)
        {
            var user = _context.Users.Include(u => u.Role).ThenInclude(r => r.Functions).FirstOrDefault(x => x.UserId == validatorId);
            return user;
        }
    }
}
