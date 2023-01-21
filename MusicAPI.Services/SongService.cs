using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Data.Models;
using MusicAPI.Services.Interfaces;
using System.Linq.Expressions;

namespace MusicAPI.Services
{
    public class SongService : ISongService
    {
        private readonly MusicDbContext dbContext;

        public SongService(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> CreateSongAsync(Song song)
        {
            this.dbContext.Songs.Add(song);

            await this.dbContext.SaveChangesAsync();

            return song.Id;
        }

        public async Task DeleteSongAsync(Guid id)
        {
            this.dbContext.Songs.Remove(new Song { Id = id });

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<Song> UpdateSongAsync(Song song)
        {
            this.dbContext.Songs.Update(song);

            await this.dbContext.SaveChangesAsync();

            return song;
        }

        public async Task<IEnumerable<T>> GetSongPageAsync<T>(int page, int perPage, Expression<Func<Song, T>> selector)
        {
            return await this.dbContext.Songs.Skip((page - 1) * perPage).Take(perPage).Select(selector).ToListAsync();
        }

        public async Task<T?> GetSongByIdAsync<T>(Guid id, Expression<Func<Song, T>> selector)
        {
            return await this.dbContext.Songs.Where(x => x.Id == id).Select(selector).FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
           => await this.dbContext.Songs.AnyAsync(x => x.Id == id);
    }
}
