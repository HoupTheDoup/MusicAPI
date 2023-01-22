using MusicAPI.Data.Models;

namespace MusicAPI.Web.Models.Album
{
    public class AlbumInputModel
    {
        public string Name { get; set; }

        public Guid ArtistId { get; set; }
    }
}
