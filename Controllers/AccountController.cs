using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Nahang.Data;
using Nahang.Data.Models;
using Nahang.Shared;

namespace Nahang.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {


        private readonly ILogger<AccountController> _logger;
        private readonly DataContext dataContext;
        private readonly UserManager<User> userManager;
        private readonly TokenService tokenService;
        private readonly JwtSetting jwtSetting;


        public AccountController(ILogger<AccountController> logger, DataContext dataContext, UserManager<User> userManager, TokenService tokenService, IOptions<JwtSetting> jwtSetting)
        {
            _logger = logger;
            this.dataContext = dataContext;
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.jwtSetting = jwtSetting.Value;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> Login([FromBody] LoginModel model)
        {
            var res = new ApiResult<string>();
            try
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user is null)
                    return new ()
                    {
                        Message = "not authorized",
                        StatusCode = HttpStatusCode.Unauthorized
                    };
                var token = tokenService.BuildToken(jwtSetting.Key, jwtSetting.Issuer, user);
                res.Data = token;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.StatusCode = HttpStatusCode.InternalServerError;
                return res;
            }

        }
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResult<string>), 200)]
        [AllowAnonymous]
        public async Task<ApiResult> Register([FromBody] RegisterModel model)
        {
            var res = new ApiResult();
            try
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user is not null)
                    return new ApiResult
                    {
                        Message = "duplicate user name",
                        StatusCode = HttpStatusCode.BadRequest
                    };
                user = new User
                {
                    Email = model.Email,
                    UserName = model.UserName
                };
               var t = await userManager.CreateAsync(user, model.Password);
                if(!t.Succeeded)
                    return new ApiResult
                    {
                        Message = "registeration failed",
                        StatusCode = HttpStatusCode.BadRequest,
                        Data = t.Errors
                    };
                var token = tokenService.BuildToken(jwtSetting.Key, jwtSetting.Issuer, user);
                res.Data = token;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.StatusCode = HttpStatusCode.InternalServerError;
                return res;
            }

        }
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> Test()
        {
            var res = new object();


            return Ok(res);
        }


    }

    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
