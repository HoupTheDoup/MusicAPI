namespace MusicAPI.Services.Interfaces
{
    public interface ISongSeederService
    {
        public Task SeedAsync(string path);
    }
}
