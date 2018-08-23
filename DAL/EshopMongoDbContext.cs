using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DAL
{
    public class EshopMongoDbContext : IEshopMongoDbContext
    {
        public IMongoDatabase Database { get; set; }

        public EshopMongoDbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            Database = client.GetDatabase("eshop");
        }
    }
}
