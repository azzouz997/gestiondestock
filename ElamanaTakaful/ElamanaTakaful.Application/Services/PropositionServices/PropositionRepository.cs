using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.ProductServices;
using ElamanaTakaful.Application.Services.SupplierServices;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Application.Services.PropositionServices
{
    public class PropositionRepository
    {
        private readonly ElamanaTakafulContext _context;
        readonly SupplierService _supplierService;
        readonly ProductService _productService;
        private readonly IDataRepository<Proposition> _propositionService;

        public PropositionRepository(ElamanaTakafulContext context, IDataRepository<Proposition> propositionService)
        {
            _context = context;
            _propositionService = propositionService;
            _supplierService = new(_context);
            _productService = new(_context);
        }

        public void ValidateProposition(int id, User validator)
        {
            Proposition proposition = _propositionService.Get(id);


            Proposition propositionPreviousData = _context.Propositions.Include(u => u.PropositionHistory).Include(p => p.QuoteHistory).FirstOrDefault(x => x.PropositionId == proposition.PropositionId);
            _context.Entry(propositionPreviousData).State = EntityState.Detached;

            List<PropositionHistory> arr = propositionPreviousData.PropositionHistory;

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


            proposition.PropositionStatus = true;
            proposition.ValidationDate = DateTime.Now;
            proposition.Validator = validator;
            _context.Propositions.Update(proposition);
            _context.SaveChanges();
        }

        public Proposition FindPropositionByPropositionNumber(int number)
        {
            return _context.Propositions.Where(p => p.PropositionNumber == number).FirstOrDefault();
        }

        public List<Proposition> FindPropositionByStartDate(DateTime startDate)
        {
            return _context.Propositions.Where(p => p.StartDate.Date == startDate.Date).ToList();
        }

        public List<Proposition> FindPropositionByEndDate(DateTime endDate)
        {
            return _context.Propositions.Where(p => p.EndDate.Date == endDate.Date).ToList();
        }

        public List<Proposition> FindPropositionByStatus(bool status)
        {
            return _context.Propositions.Where(p => p.PropositionStatus == status).ToList();
        }

        public List<Proposition> FindPropositionByDirection(string direction)
        {
            return _context.Propositions.Where(p => p.Direction.ToLower().Contains(direction.ToLower())).ToList();
        }

        public List<Proposition> FindPropositionBySupplierName(string supplierName)
        {
            return _context.Propositions.Where(p => p.Supplier.Name.ToLower().Contains(supplierName.ToLower())).ToList();
        }

        public List<Proposition> FindPropositionBySupplierNumber(string supplierCode)
        {
            return _context.Propositions.Where(p => p.Supplier.SupplierCode.ToLower().Contains(supplierCode.ToLower())).ToList();
        }

        public Proposition ActivateProposition(int propositionId)
        {
            Proposition proposition = _context.Propositions.FirstOrDefault(x => x.PropositionId == propositionId);
            proposition.PropositionStatus = true;
            _context.Propositions.Update(proposition);
            _context.SaveChanges();
            return proposition;
        }

        public Proposition DeactivateProposition(int propositionId)
        {
            Proposition proposition = _context.Propositions.FirstOrDefault(x => x.PropositionId == propositionId);
            proposition.PropositionStatus = false;
            _context.Propositions.Update(proposition);
            _context.SaveChanges();
            return proposition;
        }

        public GridFSDownloadStream DownloadFromStream(string id)
        {
            //mongoDbDatabase: establish connection from mongodb and get //Databese

            var client = new MongoClient("mongodb://mongoadmin:ElamanaTakaful@161.97.173.185:27018/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false");
            var database = client.GetDatabase("ElamanaTakafulBD");

            var gridFsBucket = new GridFSBucket(database);
            return gridFsBucket.OpenDownloadStream(ObjectId.Parse(id));
        }

        public ObjectId UploadFromStream(string fileName, Stream stream)
        {
            //mongoDbDatabase : get database of MongoDb by establishing //connection

            var client = new MongoClient("mongodb://mongoadmin:ElamanaTakaful@161.97.173.185:27018/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false");
            var database = client.GetDatabase("ElamanaTakafulBD");

            var gridFsBucket = new GridFSBucket(database);
            var id = gridFsBucket.UploadFromStream(fileName, stream);
            return id;
        }

        public void Update(Proposition proposition, string newFileId)
        {
            Proposition propositionPreviousData = _context.Propositions.Include(u => u.PropositionHistory).Include(p => p.QuoteHistory).FirstOrDefault(x => x.PropositionId == proposition.PropositionId);
            _context.Entry(propositionPreviousData).State = EntityState.Detached;
            _context.Entry(propositionPreviousData.QuoteHistory).State = EntityState.Detached;

            List<PropositionHistory> arr = propositionPreviousData.PropositionHistory;

            List<QuoteHistory> arr2 = propositionPreviousData.QuoteHistory;

            QuoteHistory quoteHistory = new();
            quoteHistory.QuoteFileId = propositionPreviousData.QuoteId;
            quoteHistory.UptadeDate = DateTime.Now;

            arr2.Add(quoteHistory);
            proposition.QuoteHistory = arr2;
            proposition.QuoteId = newFileId;

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

        public List<string> GetQuoteHistoryIds(int propositionId)
        {
            List<string> quoteHistoryIds = new();
            Proposition proposition = _propositionService.Get(propositionId);
            foreach(QuoteHistory q in proposition.QuoteHistory)
            {
                quoteHistoryIds.Add(q.QuoteFileId);
            }
            return quoteHistoryIds;
        }
        
        internal User GetUser(int? validatorId)
        {
            var user = _context.Users.Include(u => u.Role).ThenInclude(r => r.Functions).FirstOrDefault(x => x.UserId == validatorId);
            return user;
        }

    }
}
