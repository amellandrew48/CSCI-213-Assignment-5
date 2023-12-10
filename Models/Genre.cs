namespace MyMusicShop.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Music>? Musics { get; set; } 
    }
}
