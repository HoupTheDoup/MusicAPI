using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Data.Models;
using MusicAPI.Services.Interfaces;
using System.Linq.Expressions;

namespace MusicAPI.Services
{
    public class ArtistService : IArtistService
    {
        private readonly MusicDbContext dbContext;

        public ArtistService(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> CreateArtistAsync(Artist artist)
        {
            this.dbContext.Artists.Add(artist);

            await this.dbContext.SaveChangesAsync();

            return artist.Id;
        }

        public async Task DeleteArtistAsync(Guid id)
        {
            this.dbContext.Artists.Remove(new Artist { Id = id });

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<Artist> UpdateArtistAsync(Artist artist)
        {
            this.dbContext.Artists.Update(artist);

            await this.dbContext.SaveChangesAsync();

            return artist;
        }

        public async Task<IEnumerable<T>> GetArtistPageAsync<T>(int page, int perPage, Expression<Func<Artist, T>> selector)
        {
            return await this.dbContext.Artists.Skip((page - 1) * perPage).Take(perPage).Select(selector).ToListAsync();
        }

        public async Task<T?> GetArtistByIdAsync<T>(Guid id, Expression<Func<Artist, T>> selector)
        {
            return await this.dbContext.Artists.Where(x => x.Id == id).Select(selector).FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
           => await this.dbContext.Artists.AnyAsync(x => x.Id == id);
    }
}
