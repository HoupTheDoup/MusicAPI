using MusicAPI.Data.Models;
using System.Linq.Expressions;

namespace MusicAPI.Services.Interfaces
{
    public interface ISongService
    {
        public Task<Guid> CreateSongAsync(Song song);

        public Task DeleteSongAsync(Guid id);

        public Task<Song> UpdateSongAsync(Song song);

        public Task<IEnumerable<T>> GetSongPageAsync<T>(int page, int perPage, Expression<Func<Song, T>> selector);

        public Task<T?> GetSongByIdAsync<T>(Guid id, Expression<Func<Song, T>> selector);

        public Task<bool> ExistsAsync(Guid id);
    }
}
