using MusicAPI.Web.Models.Artist;
using MusicAPI.Web.Models.Genre;

namespace MusicAPI.Web.Models.Song
{
    public class SongViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public Guid AlbumId { get; set; }

        public ArtistNameViewModel[] Artists { get; set; }

        public GenreNameViewModel[] Genres { get; set; }
    }
}
