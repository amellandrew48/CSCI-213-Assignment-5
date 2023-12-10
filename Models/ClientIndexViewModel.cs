namespace MyMusicShop.Models
{
    public class ClientIndexViewModel
    {
        public List<Music> UserMusicLibrary { get; set; }
        public List<Music> AllMusic { get; set; }
        public List<CartItem> CartItems { get; set; } // Add this line

        // Add any other properties you need
    }
}
