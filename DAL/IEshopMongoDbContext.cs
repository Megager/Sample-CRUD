using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DAL
{
    public interface IEshopMongoDbContext
    {
        IMongoDatabase Database { get; set; }
    }
}
