using Microsoft.AspNetCore.Mvc;
using RDSUServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace RDSUServer.Controllers.Sportsman
{
    [Route("api/Sportsman/Info")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        [Route("HasPartner")]
        [HttpGet]
        public ActionResult<bool> BaseInfo()
        {
            int Id = UsersController.isValidJWT(Request);
            if (Id != -1)
            {
                if (AppDbContext.db.Athletes.Find(Id).Athlet == null) return false;
                else return true;
            }
            return Unauthorized();
        }

        [Route("Main")]
        [HttpGet]
        public ActionResult<Sportsmen> MainInfo()
        {
            int Id = UsersController.isValidJWT(Request);
            if (Id != -1)
            {
                Athletes athletes = AppDbContext.db.Athletes.FirstOrDefault(u => u.Id == AppDbContext.db.Users.Find(Id));
                List<Result> results = AppDbContext.db.Scorecards.ToList().Where(u => (u.Athletes1 == athletes || u.Athletes2 == athletes) 
                && (AppDbContext.db.Disqualificatons.ToList().Where(t => t.Scorecard == u).Count() == 0
                || AppDbContext.db.GoodResults.ToList().Where(t => t.Scorecard == u).Count() == 0)).ToList().
                    ConvertAll(u => new Result
                    {
                        name = u.Category.Tournament.Name,
                        date = u.Category.Tournament.Date,
                        number = u.Number,
                        count = AppDbContext.db.Scorecards.ToList().Where(t => u.Category == t.Category).Count(),
                        protocol = u.Category.Protocol,
                        category = Categories.StringCategory(u.Category.Category),
                        discvalification = AppDbContext.db.Disqualificatons.ToList().Where(t => t.Scorecard == u).Count() == 1 ?
                        AppDbContext.db.Disqualificatons.ToList().Where(t => t.Scorecard == u).ToList()[0].Period : null,
                        points = AppDbContext.db.GoodResults.ToList().Where(t => t.Scorecard == u).Count() == 1 ?
                        AppDbContext.db.GoodResults.ToList().Where(t => t.Scorecard == u).ToList()[0].Points : null,
                        position = AppDbContext.db.GoodResults.ToList().Where(t => t.Scorecard == u).Count() == 1 ?
                        AppDbContext.db.GoodResults.ToList().Where(t => t.Scorecard == u).ToList()[0].Placement : null
                    });
                return new Sportsmen
                {
                    Number = athletes.Number,
                    Trainer = athletes.Trainer.Name,
                    Athlet = athletes.Athlet.FullName + " (" + athletes.Athlet.Number + ")",
                    FullName = athletes.FullName,
                    DateOfBirth = athletes.DateOfBirth,
                    isMale = athletes.isMale,
                    Height = athletes.Height,
                    St = athletes.St,
                    La = athletes.La,
                    results = results
                };
            }
            return Unauthorized();
        }

        [Route("Parthner")]
        [HttpGet]
        public ActionResult<Sportsmen> Parthner()
        {
            int Id = UsersController.isValidJWT(Request);
            if (Id != -1)
            {
                Athletes athletes = AppDbContext.db.Athletes.FirstOrDefault(u => u.Id == AppDbContext.db.Users.Find(Id));
                Athletes parthner = athletes.Athlet;
                if (parthner != null)
                {
                    List<Result> results = AppDbContext.db.Scorecards.ToList().Where(u => ((u.Athletes1 == athletes && u.Athletes2 == parthner) || (u.Athletes2 == athletes && u.Athletes1 == parthner)) 
                    && (AppDbContext.db.Disqualificatons.ToList().Where(t => t.Scorecard == u).Count() == 0
                    || AppDbContext.db.GoodResults.ToList().Where(t => t.Scorecard == u).Count() == 0)).ToList().
                    ConvertAll(u => new Result
                    {
                        name = u.Category.Tournament.Name,
                        date = u.Category.Tournament.Date,
                        number = u.Number,
                        count = AppDbContext.db.Scorecards.ToList().Where(t => u.Category == t.Category).Count(),
                        protocol = u.Category.Protocol,
                        category = Categories.StringCategory(u.Category.Category),
                        discvalification = AppDbContext.db.Disqualificatons.ToList().Where(t => t.Scorecard == u).Count() == 1 ?
                        AppDbContext.db.Disqualificatons.ToList().Where(t => t.Scorecard == u).ToList()[0].Period : null,
                        points = AppDbContext.db.GoodResults.ToList().Where(t => t.Scorecard == u).Count() == 1 ?
                        AppDbContext.db.GoodResults.ToList().Where(t => t.Scorecard == u).ToList()[0].Points : null,
                        position = AppDbContext.db.GoodResults.ToList().Where(t => t.Scorecard == u).Count() == 1 ?
                        AppDbContext.db.GoodResults.ToList().Where(t => t.Scorecard == u).ToList()[0].Placement : null
                    });
                    return new Sportsmen
                    {
                        Number = athletes.Number,
                        FullName = athletes.FullName,
                        DateOfBirth = athletes.DateOfBirth,
                        isMale = athletes.isMale,
                        Height = athletes.Height,
                        St = athletes.St,
                        La = athletes.La,
                        results = results
                    };
                }
            }
            return Unauthorized();
        }

        [Route("Trainer")]
        [HttpGet]
        public ActionResult<Trainers> Trainer()
        {
            int Id = UsersController.isValidJWT(Request);
            if (Id != -1)
            {
                Athletes athletes = AppDbContext.db.Athletes.FirstOrDefault(u => u.Id == AppDbContext.db.Users.Find(Id));
                if (athletes != null)
                {
                    return new Trainers
                    {
                        Name = athletes.Trainer.Name,
                        Phone = athletes.Trainer.Phone,
                        TrainerFullName = athletes.Trainer.TrainerFullName,
                        Location = athletes.Trainer.Location
                    };
                }
            }
            return Unauthorized();
        }

        [Route("ChangeHeight")]
        [HttpPut]
        public async Task<IActionResult> ChangeHeight(int height)
        {
            int Id = UsersController.isValidJWT(Request);
            if (Id != -1)
            {
                Athletes athletes = AppDbContext.db.Athletes.FirstOrDefault(u => u.Id == AppDbContext.db.Users.Find(Id));
                athletes.Height = height;
                await AppDbContext.db.SaveChangesAsync();
            }
            return Unauthorized();
        }

        [Route("RemoveParthner")]
        [HttpPut]
        public async Task<IActionResult> RemoveParthner()
        {
            int Id = UsersController.isValidJWT(Request);
            if (Id != -1)
            {
                Athletes athletes = AppDbContext.db.Athletes.FirstOrDefault(u => u.Id == AppDbContext.db.Users.Find(Id));
                athletes.Athlet = null;
                await AppDbContext.db.SaveChangesAsync();
            }
            return Unauthorized();
        }

        [Route("RemoveMe")]
        [HttpPut]
        public async Task<IActionResult> RemoveMe()
        {
            int Id = UsersController.isValidJWT(Request);
            if (Id != -1)
            {
                Athletes athletes = AppDbContext.db.Athletes.FirstOrDefault(u => u.Id == AppDbContext.db.Users.Find(Id));
                athletes.Status = -1;
                await AppDbContext.db.SaveChangesAsync();
            }
            return Unauthorized();
        }

        public struct Sportsmen
        {
            public int Number;

            public string Trainer;

            public string Athlet;

            public string FullName;

            public DateTime DateOfBirth;

            public bool isMale;

            public int? Height;

            public byte St;

            public byte La;

            public List<Result> results;
        }


        public struct Result
        {
            public string name;
            public DateTime date;
            public int count;
            public int? position;
            public int? discvalification;
            public int? points;
            public string protocol;
            public string category;
            public int number;
        }

        public struct Trainers
        {

            public string Name;

            public string TrainerFullName;

            public string Location;

            public string Phone;
        }
    }
}
