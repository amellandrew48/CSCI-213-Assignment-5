namespace MyMusicShop.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
        public virtual ICollection<CartItem> CartItems { get; set; } 
        public virtual ICollection<Music> MusicLibrary { get; set; } 
    }
}
