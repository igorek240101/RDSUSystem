using Microsoft.AspNetCore.Mvc;
using RDSUServer.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RDSUServer.Controllers
{
    [Route("api/Admin")]
    public class AdminController : ControllerBase
    {
        [Route("RegisterRDSU")]
        [HttpPost]
        public async Task<IActionResult> Register(string Email)
        {
            if (ModelState.IsValid)
            {
                Users user = AppDbContext.db.Users.FirstOrDefault(u => u.Email == Email);
                string password = "";
                if (user == null)
                {
                    Random random = new Random();
                    int count = random.Next(4, 10);
                    for (int i = 0; i < count; i++)
                    {
                        password += (char)random.Next(30, 100);
                    }
                    SHA512Managed sha = new SHA512Managed();
                    string hash = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
                    user = new Users { Email = Email, Status = true, Password = hash };
                    AppDbContext.db.Users.Add(user);
                }
                RDSUWorkers worker = AppDbContext.db.RDSUWorkers.FirstOrDefault(u => u.User == user);
                if (worker != null)
                {
                    AppDbContext.db.RDSUWorkers.Add(new RDSUWorkers { Status = true, User = user });
                    UsersController.PasswordMessage(Email, password);
                    await AppDbContext.db.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            return UnprocessableEntity();
        }

        [Route("ChangePassword")]
        [HttpPut]
        public async Task<IActionResult> ChangePassword(string Email)
        {
            Random random = new Random();
            int count = random.Next(4, 10);
            string password = "";
            for (int i = 0; i < count; i++)
            {
                password += (char)random.Next(30, 100);
            }
            SHA512Managed sha = new SHA512Managed();
            string hash = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
            Users user = AppDbContext.db.Users.FirstOrDefault(u => u.Email == Email);
            user.Password = hash;
            await AppDbContext.db.SaveChangesAsync();
            UsersController.PasswordMessage(Email, password);
            return Ok();
        }
    }
}
