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
    public class ArtistController : ControllerBase
    {

        private readonly IArtistService artistService;

        public ArtistController(IArtistService artistService)
        {
            this.artistService = artistService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var model = await this.artistService.GetArtistByIdAsync(id, x => new ArtistViewModel { Id = x.Id, Name = x.Name, IsGroup = x.IsGroup, Songs = x.Songs.Select(y => new SongViewModel { Name = y.Name }).ToArray() });

            if (model == null)
            {
                return this.NotFound();
            }

            return this.Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetGenrePageAsync(
            [FromQuery][Range(0, int.MaxValue)] int page = 1,
            [FromQuery][Range(5, 100)] int perPage = 5)
        {
            var artists = await this.artistService.GetArtistPageAsync(page, perPage, x => new ArtistViewModel { Id = x.Id, Name = x.Name, IsGroup = x.IsGroup, Songs = x.Songs.Select(y => new SongViewModel { Name = y.Name }).ToArray() });

            return this.Ok(artists);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateArtistAsync(ArtistInputModel model)
        {
            var artist = new Artist { Name = model.Name, IsGroup = model.IsGroup };
            var id = await this.artistService.CreateArtistAsync(artist);

            return this.CreatedAtAction(
                nameof(this.Get),
                new
                {
                    id = id.ToString()
                });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateArtistAsync([FromRoute] Guid id, ArtistInputModel model)
        {
            bool exists = this.artistService.ExistsAsync(id).Result;

            if (!exists)
            {
                return this.NotFound();
            }
            var artist = new Artist { Id = id, Name = model.Name, IsGroup = model.IsGroup };

            await this.artistService.UpdateArtistAsync(artist);

            return this.Ok(artist);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteArtistAsync(Guid id)
        {
            bool exists = await this.artistService.ExistsAsync(id);

            if (!exists)
            {
                return this.NotFound();
            }

            await this.artistService.DeleteArtistAsync(id);

            return this.Ok();
        }
    }
}
