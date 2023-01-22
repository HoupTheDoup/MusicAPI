using MusicAPI.Data.Models;
using MusicAPI.Web.Models.Artist;
using MusicAPI.Web.Models.Song;

namespace MusicAPI.Web.Models.Album
{
    public class AlbumViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ArtistViewModel Artist { get; set; }

        public SongViewModel[] Songs { get; set; }
    }
}
