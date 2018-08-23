using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
    public interface IEshopDbContext
    {
         EshopDbContext Context { get; }
         DbSet<Image> Images { get; set; }
         DbSet<HeadPhone> HeadPhones { get; set; }
         DbSet<Loudspeaker> Loudspeakers { get; set; }
         DbSet<MediaPlayer> MediaPlayers { get; set; }
         DbSet<Product> Products { get; set; }
         DbSet<Scope> Scopes { get; set; }
         DbSet<TradeMark> TradeMarks { get; set; }
         DbSet<Entities.Type> Types { get; set; }
         DbSet<View> Views { get; set; }
         DbSet<Order> Orders { get; set; }
         DbSet<ProductType> ProductTypes { get; set; }
         DbSet<SoundSystem> SoundSystems { get; set; }
    }
}