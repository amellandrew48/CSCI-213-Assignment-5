using Microsoft.EntityFrameworkCore;
using MyMusicShop.Models; 


namespace MusicShop.Data
{
    public class MusicShopContext : DbContext
    {
        public MusicShopContext(DbContextOptions<MusicShopContext> options) : base(options)
        {
        }

        public DbSet<Music> Musics { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Music>()
                .Property(m => m.Price)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
