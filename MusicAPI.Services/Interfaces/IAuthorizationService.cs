using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAPI.Services.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<IdentityResult?> RegisterUserAsync(string username, string password);

        public Task<string?> LoginAsync(string username, string password);

    }
}
