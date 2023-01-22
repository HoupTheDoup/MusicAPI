using CsvHelper.Configuration;
using MusicAPI.Services.Models;

namespace MusicAPI.Services.CSVMaps
{
    public class SongCSVMap : ClassMap<SongCSVDto>
    {
        public SongCSVMap()
        {
            Map(m => m.Name).Name("name");

            Map(m => m.Album).Name("album");

            Map(m => m.AlbumId).Name("album_id");

            Map(m => m.Year).Name("year");

            Map(m => m.Artists).Convert(row => row.Row.GetField("artists")!
            .Trim(new char[] { '[', ']' })
            .Split(", ")
            .Select(x => x.Trim(new char[] { '\'' }))
            .ToArray());

            Map(m => m.ArtistIds).Convert(row => row.Row.GetField("artist_ids")!
                .Trim(new char[] { '[', ']' })
                .Split(", ")
                .Select(x => x.Trim(new char[] { '\'' }))
                .ToArray());
        }
    }
}
