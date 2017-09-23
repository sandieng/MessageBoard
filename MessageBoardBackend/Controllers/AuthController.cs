using MessageBoardBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace MessageBoardBackend.Controllers
{
    [Produces("application/json")]
    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly ApiContext context;

        public class JwtPacket
        {
            public string Token { get; set; }
            public string FirstName { get; set; }
        }

        public AuthController(ApiContext context)
        {
            this.context = context;
        }

        [HttpPost("register")]
        public JwtPacket Register([FromBody] User user)
        {
            var jwt = new JwtSecurityToken();
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            context.Users.Add(user);
            context.SaveChanges();

            return new JwtPacket
            {
                Token = encodedJwt,
                FirstName = user.FirstName
            };
        }
    }
}