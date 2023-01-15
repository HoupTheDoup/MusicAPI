using MusicAPI.Data.Models;
using System.Linq.Expressions;

namespace MusicAPI.Services.Interfaces
{
    public interface IGenreService
    {
        public Task<Guid> CreateGenreAsync(Genre genre);

        public Task DeleteGenreAsync(Guid id);

        public Task<Genre> UpdateGenreAsync(Genre genre);

        public Task<IEnumerable<T>> GetGenrePageAsync<T>(int page, int perPage, Expression<Func<Genre, T>> selector);

        public Task<T?> GetGenreByIdAsync<T>(Guid id, Expression<Func<Genre, T>> selector);

        public Task<bool> ExistsAsync(Guid id);
    }
}
