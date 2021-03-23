using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDSUServer.Models;

namespace RDSUServer.Controllers
{
    [Route("api/News]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        [Route("Our")]
        [HttpGet]
        public IEnumerable<News> OurNews()
        {
            return AppDbContext.db.News.ToList().Where(u => u.Trainer == null);
        }

        [Route("My")]
        [HttpGet]
        public IEnumerable<News> MyNews()
        {
            return AppDbContext.db.News.ToList().Where(u => u.Trainer == null);
        }
    }
}
