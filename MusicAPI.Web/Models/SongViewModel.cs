﻿namespace MusicAPI.Web.Models
{
    public class SongViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public Guid AlbumId { get; set; }

        public ArtistViewModel[] Artists { get; set; }

        public GenreViewModel[] Genres { get; set; }
    }
}
