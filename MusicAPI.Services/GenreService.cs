using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Data.Models;
using MusicAPI.Services.Interfaces;
using System.Linq.Expressions;

namespace MusicAPI.Services
{
    public class GenreService : IGenreService
    {
        private readonly MusicDbContext dbContext;

        public GenreService(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> CreateGenreAsync(Genre genre)
        {
            this.dbContext.Genres.Add(genre);

            await this.dbContext.SaveChangesAsync();

            return genre.Id;
        }

        public async Task DeleteGenreAsync(Guid id)
        {
            var genreSong = await dbContext.SongGenres.Where(x => x.GenreId == id).ToArrayAsync();
            this.dbContext.RemoveRange(genreSong);

            this.dbContext.Genres.Remove(new Genre { Id = id });

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<Genre> UpdateGenreAsync(Genre genre)
        {
            this.dbContext.Genres.Update(genre);

            await this.dbContext.SaveChangesAsync();

            return genre;
        }

        public async Task<IEnumerable<T>> GetGenrePageAsync<T>(int page, int perPage, Expression<Func<Genre, T>> selector)
        {
            return await this.dbContext.Genres.Skip((page - 1) * perPage).Take(perPage).Select(selector).ToListAsync();
        }

        public async Task<T?> GetGenreByIdAsync<T>(Guid id, Expression<Func<Genre, T>> selector)
        {
            return await this.dbContext.Genres.Where(x => x.Id == id).Select(selector).FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
           => await this.dbContext.Genres.AnyAsync(x => x.Id == id);


    }
}
