using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.Data.Models;
using MusicAPI.Services.Interfaces;
using MusicAPI.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace MusicAPI.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var model = await this.genreService.GetGenreByIdAsync(id, x => new GenreViewModel { Id = x.Id, Name = x.Name, Songs = x.Songs.Select(y => new SongViewModel { Name = y.Song.Name }).ToArray()});

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
            var genres = await this.genreService.GetGenrePageAsync(page, perPage, x => new GenreViewModel { Id = x.Id, Name = x.Name, Songs = x.Songs.Select(y => new SongViewModel { Name = y.Song.Name}).ToArray()});

            return this.Ok(genres);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateGenreAsync(GenreInputModel model)
        {
            var genre = new Genre { Name = model.Name };
            var id = await this.genreService.CreateGenreAsync(genre);

            return this.CreatedAtAction(
                nameof(this.Get),
                new
                {
                    id = id.ToString()
                }) ;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateGenreAsync([FromRoute] Guid id, GenreInputModel model)
        {
            bool exists = await this.genreService.ExistsAsync(id);

            if (!exists)
            {
                return this.NotFound();
            }
            var genre = new Genre { Id = id, Name = model.Name };

            await this.genreService.UpdateGenreAsync(genre);

            return this.Ok(genre);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteArtistAsync(Guid id)
        {
            bool exists = await this.genreService.ExistsAsync(id);

            if (!exists)
            {
                return this.NotFound();
            }

            await this.genreService.DeleteGenreAsync(id);

            return this.Ok();
        }

    }
}
