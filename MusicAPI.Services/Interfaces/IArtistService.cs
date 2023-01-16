using MusicAPI.Data.Models;
using System.Linq.Expressions;

namespace MusicAPI.Services.Interfaces
{
    public interface IArtistService
    {
        public Task<Guid> CreateArtistAsync(Artist artist);

        public Task DeleteArtistAsync(Guid id);

        public Task<Artist> UpdateArtistAsync(Artist artist);

        public Task<IEnumerable<T>> GetArtistPageAsync<T>(int page, int perPage, Expression<Func<Artist, T>> selector);

        public Task<T?> GetArtistByIdAsync<T>(Guid id, Expression<Func<Artist, T>> selector);

        public Task<bool> ExistsAsync(Guid id);
    }
}
