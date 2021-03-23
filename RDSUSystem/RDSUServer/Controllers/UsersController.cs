using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RDSUServer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RDSUServer.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {

        public static Timer timerstart = new Timer();

        private static Timer timer;

        public static void TimerStart(object sender, ElapsedEventArgs e)
        {
            timerstart.Stop();
            timerstart = null;
            TimerTick(null, null);
            timer = new Timer(86400000);
            timer.Elapsed += new ElapsedEventHandler(TimerTick);
            timer.Start();
        }

        public static async void TimerTick(object sender, ElapsedEventArgs e)
        {
            foreach (var element in AppDbContext.db.Athletes.ToList().Where(u => u.Status > 0))
            {
                element.Status--;
            }
            await AppDbContext.db.SaveChangesAsync();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(Users model)
        {
            if (ModelState.IsValid)
            {
                SHA512Managed sha = new SHA512Managed();
                string hash = System.Text.Encoding.UTF8.GetString(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password)));
                Users user = AppDbContext.db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == hash);
                if (user != null)
                {
                    byte pass = 0;
                    Athletes a = AppDbContext.db.Athletes.FirstOrDefault(u => u.Id == user);
                    Trainers t = AppDbContext.db.Trainers.FirstOrDefault(u => u.User == user);
                    RDSUWorkers r = AppDbContext.db.RDSUWorkers.FirstOrDefault(u => u.User == user);
                    if (a != null && a.Status >= 0) pass &= 0b00000001;
                    if (t != null && t.Status) pass &= 0b00000010;
                    if (r != null && r.Status) pass &= 0b00000100;
                    return Ok(new { token = GenerateJWT(user, pass) });
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль"); // : проверка логина и пароля отдельно
            }
            return UnprocessableEntity();
        }

        [Route("ChangeEmail")]
        [HttpPut]
        public async Task<IActionResult> ChangeEmail(string Email)
        {
            int Id = isValidJWT(Request);
            if (Id != -1)
            {
                Users user = AppDbContext.db.Users.Find(Id);
                user.Email = Email;
                await AppDbContext.db.SaveChangesAsync();
                return Ok();
            }
            return Unauthorized();
        }

        [Route("ChangePassword")]
        [HttpPut]
        public async Task<IActionResult> ChangePassword(string Password)
        {
            int Id = isValidJWT(Request);
            if (Id != -1)
            {
                Users user = AppDbContext.db.Users.Find(Id);
                user.Password = Password;
                await AppDbContext.db.SaveChangesAsync();
                return Ok();
            }
            return Unauthorized();
        }

        private string GenerateJWT(Users user, byte pass)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ServerInfo.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            var claims = new List<Claim>()
            {
                new Claim("jti",user.Id.ToString()),
                new Claim("role",Convert.ToString(pass))
            };
            JwtSecurityToken token = new JwtSecurityToken(ServerInfo.Issuer, ServerInfo.Audience, claims, DateTime.Now, DateTime.Now.AddSeconds(ServerInfo.TokenLifetime), credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static int isValidJWT(HttpRequest request)
        {
            if (request.IsHttps)
            {
                IHeaderDictionary header = request.Headers;
                string s = header["Authorization"];
                if (s != null)
                {
                    JwtSecurityTokenHandler jwt = new JwtSecurityTokenHandler();
                    if (s.Length > 7 && jwt.CanReadToken(s.Substring(7)))
                    {
                        JwtSecurityToken token = jwt.ReadJwtToken((s).Substring(7));
                        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ServerInfo.Secret));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                        var claims = new List<Claim>()
                        {
                            new Claim("jti",token.Id)
                        };
                        JwtSecurityToken validtoken = new JwtSecurityToken(token.Issuer, token.Audiences.First(), claims, token.ValidFrom, token.ValidTo, credentials);
                        validtoken = (new JwtSecurityTokenHandler()).ReadJwtToken(new JwtSecurityTokenHandler().WriteToken(validtoken));
                        if (token.Issuer == ServerInfo.Issuer && token.Audiences.First() == ServerInfo.Audience && token.RawSignature == validtoken.RawSignature && token.ValidTo < DateTime.Now)
                        {
                            return Convert.ToInt32(token.Id);
                        }
                    }
                }
            }
            return -1;
        }

        public static void PasswordMessage(string Email, string password)
        {
        }

        private static class ServerInfo
        {
            public static readonly string Issuer = "Igor";

            public static readonly string Audience = "Fedor";

            public static readonly int TokenLifetime = 3600;

            public static string Secret;

            static ServerInfo()
            {
                FileStream file = new FileStream("Secret.TTST", FileMode.Open);
                StreamReader reader = new StreamReader(file);
                Secret = reader.ReadToEnd();
                reader.Close();
                file.Close();
            }
        }
    }
}
