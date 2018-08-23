using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DAL.Entities.Mongo
{
    public class Comment : IMongoEntity
    {
        public ObjectId Id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
    }
}
