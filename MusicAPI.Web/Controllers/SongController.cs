using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.Data.Models;
using MusicAPI.Services.Interfaces;
using MusicAPI.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicAPI.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongService songService;

        public SongController(ISongService songService)
        {
            this.songService = songService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var model = await this.songService.GetSongByIdAsync(id, x => new SongViewModel
            {
                Id = x.Id,
                Name = x.Name,
                //Artist = new ArtistViewModel { Name = x.Artist.Name },
                // Songs = x.Songs.Select(y => new SongViewModel { Name = y.Name }).ToArray()
            });

            if (model == null)
            {
                return this.NotFound();
            }

            return this.Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetSongPageAsync(
            [FromQuery][Range(0, int.MaxValue)] int page = 1,
            [FromQuery][Range(5, 100)] int perPage = 5)
        {
            var songs = await this.songService.GetSongPageAsync(page, perPage, x => new SongViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Year = x.Year,
                AlbumId = x.AlbumId,
                Artists = x.Artists.Select(y => new ArtistViewModel { Name = y.Artist.Name, Id = y.Artist.Id }).ToArray(),
                Genres = x.Genres.Select(z => new GenreViewModel { Name = z.Genre.Name, Id = z.Genre.Id }).ToArray()
            });

            return this.Ok(songs);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateSongAsync(SongInputModel model)
        {
            var song = new Song
            {
                Name = model.Name,
                Year = model.Year,
                AlbumId = model.AlbumId,
                Artists = model.Artists.Select(x => new SongArtist { ArtistId = x }).ToArray(),
                Genres = model.Genres.Select(y => new SongGenre { GenreId = y }).ToArray()
            };
            var id = await this.songService.CreateSongAsync(song);

            return this.CreatedAtAction(
                nameof(this.Get),
                new
                {
                    id = id.ToString()
                });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateSongAsync([FromRoute] Guid id, SongInputModel model)
        {
            bool exists = await this.songService.ExistsAsync(id);

            if (!exists)
            {
                return this.NotFound();
            }
            var song = new Song
            {
                Name = model.Name,
                Year = model.Year,
                AlbumId = model.AlbumId,
                Artists = model.Artists.Select(x => new SongArtist { ArtistId = x }).ToArray(),
                Genres = model.Genres.Select(y => new SongGenre { GenreId = y }).ToArray()
            };

            await this.songService.UpdateSongAsync(song);

            return this.Ok(song);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteSongAsync(Guid id)
        {
            bool exists = await this.songService.ExistsAsync(id);

            if (!exists)
            {
                return this.NotFound();
            }

            await this.songService.DeleteSongAsync(id);

            return this.Ok();
        }
    }
}
