using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Data.Models;
using MusicAPI.Services.Interfaces;
using System.Linq.Expressions;

namespace MusicAPI.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly MusicDbContext dbContext;

        public AlbumService(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> CreateAlbumAsync(Album album)
        {
            this.dbContext.Albums.Add(album);

            await this.dbContext.SaveChangesAsync();

            return album.Id;
        }

        public async Task DeleteAlbumAsync(Guid id)
        {
            this.dbContext.Albums.Remove(new Album { Id = id });

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<Album> UpdateAlbumAsync(Album album)
        {
            this.dbContext.Albums.Update(album);

            await this.dbContext.SaveChangesAsync();

            return album;
        }

        public async Task<IEnumerable<T>> GetAlbumPageAsync<T>(int page, int perPage, Expression<Func<Album, T>> selector)
        {
            return await this.dbContext.Albums.Skip((page - 1) * perPage).Take(perPage).Select(selector).ToListAsync();
        }

        public async Task<T?> GetAlbumByIdAsync<T>(Guid id, Expression<Func<Album, T>> selector)
        {
            return await this.dbContext.Albums.Where(x => x.Id == id).Select(selector).FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
           => await this.dbContext.Albums.AnyAsync(x => x.Id == id);
    }
}
