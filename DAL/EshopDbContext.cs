using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EshopDbContext : IdentityDbContext<Login>, IEshopDbContext
    {
        public EshopDbContext() : base("DbConnection")
        {
        }

        public EshopDbContext Context => this;
        public DbSet<Image> Images { get; set; }
        public DbSet<HeadPhone> HeadPhones { get; set; }
        public DbSet<Loudspeaker> Loudspeakers { get; set; }
        public DbSet<MediaPlayer> MediaPlayers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<TradeMark> TradeMarks { get; set; }
        public DbSet<Entities.Type> Types { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<SoundSystem> SoundSystems { get; set; }

        public static EshopDbContext Create()
        {
            return new EshopDbContext();
        }
    }
}
