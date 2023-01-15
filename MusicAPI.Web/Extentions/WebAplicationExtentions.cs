namespace MusicAPI.Web.Extensions
{
    using Microsoft.AspNetCore.Identity;

    public static class WebApplicationExtensions
    {
        public static async Task SeedRolesAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (roleManager.Roles.Any())
            {
                return;
            }

            var adminRole = CreateRole("admin");
            var userRole = CreateRole("user");

            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(userRole);
        }

        public static async Task SeedUsersAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (userManager.Users.Any())
            {
                return;
            }

            var admin = CreateUser("admin@admin.com");

            await userManager.CreateAsync(admin, "admin123");
            await userManager.AddToRoleAsync(admin, "admin");
        }

        private static IdentityRole CreateRole(string name)
            => new IdentityRole
            {
                Name = name,
                NormalizedName = name.ToUpper(),
            };

        private static IdentityUser CreateUser(string email, params string[] roles)
            => new IdentityUser
            {
                UserName = email,
                Email = email,
            };
    }
}