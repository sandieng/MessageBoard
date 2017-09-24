using MessageBoardBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

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

        public class LoginData
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }


        public AuthController(ApiContext context)
        {
            this.context = context;
        }

        [HttpPost("register")]
        public JwtPacket Register([FromBody] User user)
        {
            context.Users.Add(user);
            context.SaveChanges();

            var jwt = new JwtSecurityToken();
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return CreateJwtPacket(user);
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginData loginData)
        {
            var user = context.Users.SingleOrDefault(x => x.Email == loginData.Email && x.Password == loginData.Password);

            if (user == null)
                return NotFound("email or password incorrect");

            return Ok(CreateJwtPacket(user));
        }

        private JwtPacket CreateJwtPacket(User user)
        {
            var jwt = new JwtSecurityToken();
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtPacket
            {
                Token = encodedJwt,
                FirstName = user.FirstName
            };
        }
    }
}