namespace MusicAPI.Data.Models
{
    public class SongArtist
    { 
        public Guid SongId { get; set; }
    
        public Song Song { get; set; }
    
        public Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

    }
}
