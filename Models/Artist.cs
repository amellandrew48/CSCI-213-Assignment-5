

namespace MyMusicShop.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Music> Musics { get; set; }
    }
}