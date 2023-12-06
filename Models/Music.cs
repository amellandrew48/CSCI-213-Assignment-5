namespace MyMusicShop.Models
{
    public class Music
    {
        public int MusicId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        
        public int GenreId { get; set; }
        public int ArtistId { get; set; }

       
        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } 
    }
}
