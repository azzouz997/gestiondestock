using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Application.Services.OrderServices
{
    public class OrderRepository
    {
        private readonly ElamanaTakafulContext _context;
        public OrderRepository(ElamanaTakafulContext context)
        {
            _context = context;
        }

        public List<Order> GetOrdersByCreator(int creatorId)
        {
            return _context.Orders.Where(o => o.CreatorId == creatorId).ToList();
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

        public GridFSDownloadStream DownloadFromStream(string id)
        {
            //mongoDbDatabase: establish connection from mongodb and get //Databese

            var client = new MongoClient("mongodb://mongoadmin:ElamanaTakaful@161.97.173.185:27018/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false");
            var database = client.GetDatabase("ElamanaTakafulBD");

            var gridFsBucket = new GridFSBucket(database);
            return gridFsBucket.OpenDownloadStream(ObjectId.Parse(id));
        }
    }
}
