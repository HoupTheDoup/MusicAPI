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
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService albumService;

        public AlbumController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var model = await this.albumService.GetAlbumByIdAsync(id, x => new AlbumViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Artist = new ArtistViewModel { Name = x.Artist.Name },
                Songs = x.Songs.Select(y => new SongViewModel { Name = y.Name }).ToArray()
            });

            if (model == null)
            {
                return this.NotFound();
            }

            return this.Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbumPageAsync(
            [FromQuery][Range(0, int.MaxValue)] int page = 1,
            [FromQuery][Range(5, 100)] int perPage = 5)
        {
            var albums = await this.albumService.GetAlbumPageAsync(page, perPage, x => new AlbumViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Artist = new ArtistViewModel { Name = x.Artist.Name, Id = x.ArtistId },
                Songs = x.Songs.Select(y => new SongViewModel { Name = y.Name }).ToArray()
            });

            return this.Ok(albums);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAlbumAsync(AlbumInputModel model)
        {
            var album = new Album { Name = model.Name, ArtistId = model.ArtistId };
            var id = await this.albumService.CreateAlbumAsync(album);

            return this.CreatedAtAction(
                nameof(this.Get),
                new
                {
                    id = id.ToString()
                });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateAlbumAsync([FromRoute] Guid id, AlbumInputModel model)
        {
            bool exists = await this.albumService.ExistsAsync(id);

            if (!exists)
            {
                return this.NotFound();
            }
            var album = new Album { Id = id, Name = model.Name, ArtistId = model.ArtistId};

            await this.albumService.UpdateAlbumAsync(album);

            return this.Ok(album);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAlbumAsync(Guid id)
        {
            bool exists = await this.albumService.ExistsAsync(id);

            if (!exists)
            {
                return this.NotFound();
            }

            await this.albumService.DeleteAlbumAsync(id);

            return this.Ok();
        }
    }
}
