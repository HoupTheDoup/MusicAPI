using MusicAPI.Data.Models;
using System.Linq.Expressions;

namespace MusicAPI.Services.Interfaces
{
    public interface IAlbumService
    {
        public Task<Guid> CreateAlbumAsync(Album album);

        public Task DeleteAlbumAsync(Guid id);

        public Task<Album> UpdateAlbumAsync(Album album);

        public Task<IEnumerable<T>> GetAlbumPageAsync<T>(int page, int perPage, Expression<Func<Album, T>> selector);

        public Task<T?> GetAlbumByIdAsync<T>(Guid id, Expression<Func<Album, T>> selector);

        public Task<bool> ExistsAsync(Guid id);
    }
}
