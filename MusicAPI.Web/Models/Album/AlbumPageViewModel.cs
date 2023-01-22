using MusicAPI.Web.Models.Artist;

namespace MusicAPI.Web.Models.Album
{
    public class AlbumPageViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ArtistNameViewModel Artist { get; set; }
    }
}
