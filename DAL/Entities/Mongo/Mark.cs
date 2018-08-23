using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DAL.Entities.Mongo
{
    public class Mark : IMongoEntity
    {
        public ObjectId Id { get; set; }
        public int ProductMark { get; set; }
        public string UserId { get; set; }
    }
}
