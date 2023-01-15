namespace MusicAPI.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MusicAPI.Services.Interfaces;
    using MusicAPI.Web.Models.Users;

    public class UserController : ControllerBase
    {
        private readonly IAuthorizationService authorizationService;

        public UserController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            var registerResult = await this.authorizationService.RegisterUserAsync(model.Username, model.Password);

            return this.Ok(registerResult);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            var token = await this.authorizationService.LoginAsync(model.Username, model.Password);

            if (token == null)
            {
                return this.Unauthorized();
            }

            return this.Ok(token);
        }
    }
}
