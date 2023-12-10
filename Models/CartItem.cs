namespace MyMusicShop.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }

        //fk's
        public int UserId { get; set; }
        public int MusicId { get; set; }

        
        public virtual User? User { get; set; }
        public virtual Music? Music { get; set; }
    }
}
