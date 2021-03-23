using Microsoft.AspNetCore.Mvc;
using RDSUServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDSUServer.Controllers.Sportsman
{
    [Route("api/RegisterToTournament")]
    [ApiController]
    public class RegisterToTournamentController : ControllerBase
    {
        [Route("Tournament")]
        [HttpGet]
        public ActionResult<List<Tournament>> TournamentInfo()
        {
            int Id = UsersController.isValidJWT(Request);
            if (Id != -1)
            {
                Athletes athletes = AppDbContext.db.Athletes.FirstOrDefault(u => u.Id.Id == Id);
                Athletes parthner = athletes.Athlet;
                byte old = athletes.DateOfBirth > parthner.DateOfBirth ? Categories.OldReader(DateTime.Today.Year - athletes.DateOfBirth.Year) : Categories.OldReader(DateTime.Today.Year - parthner.DateOfBirth.Year);
                byte danceSt = athletes.St > parthner.St ? (byte)(athletes.St * old) : (byte)(parthner.St * old);
                byte danceLa = athletes.La > parthner.La ? (byte)(athletes.La * old) : (byte)(parthner.La * old);
                return AppDbContext.db.Tournaments.ToList().Where(
                    u => u.Date >= DateTime.Today.AddDays(athletes.Status) &&
                    u.Date >= DateTime.Today.AddDays(parthner.Status) && athletes.Status != -1 && parthner.Status != -1 &&
                    AppDbContext.db.Categories.ToList().Where(
                        t => t.Tournament == u
                        && (Categories.Categorieser(false, danceLa, old) == t.Category || Categories.Categorieser(true, danceSt, old) == t.Category)
                        && AppDbContext.db.Scorecards.ToList().Where(g => g.Category == t && g.Athletes1 == athletes || g.Athletes1 == parthner || g.Athletes2 == athletes || g.Athletes2 == parthner).Count() == 0
                        ).Count() > 0).ToList().ConvertAll(u => new Tournament { name = u.Name, id = u.Id });
            }
            return Unauthorized();
        }

        [Route("Category")]
        [HttpGet]
        public ActionResult<List<Category>> CategoryInfo(int ID)
        {
            int Id = UsersController.isValidJWT(Request);
            if (Id != -1)
            {
                Athletes athletes = AppDbContext.db.Athletes.FirstOrDefault(u => u.Id.Id == Id);
                Athletes parthner = athletes.Athlet;
                byte old = athletes.DateOfBirth > parthner.DateOfBirth ? Categories.OldReader(DateTime.Today.Year - athletes.DateOfBirth.Year) : Categories.OldReader(DateTime.Today.Year - parthner.DateOfBirth.Year);
                byte danceSt = athletes.St > parthner.St ? athletes.St : parthner.St;
                byte danceLa = athletes.La > parthner.La ? athletes.La : parthner.La;
                return AppDbContext.db.Categories.ToList().Where(t => t.Tournament.Id == ID && (Categories.Categorieser(false, danceLa, old) == t.Category || Categories.Categorieser(true, danceSt, old) == t.Category)
                && AppDbContext.db.Scorecards.ToList().Where(u => u.Category == t && u.Athletes1 == athletes || u.Athletes1 == parthner || u.Athletes2 == athletes || u.Athletes2 == parthner).Count() == 0).ToList().ConvertAll(u => new Category { name = Categories.StringCategory(u.Category), id = u.Id });
            }
            return Unauthorized();
        }

        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<int>> Register(int ID)
        {
            int Id = UsersController.isValidJWT(Request);
            if (Id != -1)
            {
                Athletes athletes = AppDbContext.db.Athletes.FirstOrDefault(u => u.Id.Id == Id);
                Athletes parthner = athletes.Athlet;
                Categories categories = AppDbContext.db.Categories.Find(ID);
                byte old = athletes.DateOfBirth > parthner.DateOfBirth ? Categories.OldReader(DateTime.Today.Year - athletes.DateOfBirth.Year) : Categories.OldReader(DateTime.Today.Year - parthner.DateOfBirth.Year);
                byte danceSt = athletes.St > parthner.St ? athletes.St : parthner.St;
                byte danceLa = athletes.La > parthner.La ? athletes.La : parthner.La;
                if (categories.Tournament.Date >= DateTime.Today.AddDays(athletes.Status) && categories.Tournament.Date >= DateTime.Today.AddDays(parthner.Status))
                {
                    if (categories.Category == Categories.Categorieser(false, danceLa, old) || Categories.Categorieser(true, danceSt, old) == categories.Category)
                    {
                        if (athletes.Status != -1 && parthner.Status != -1)
                        {
                            if (AppDbContext.db.Scorecards.ToList().Where(u => u.Category == categories && u.Athletes1 == athletes || u.Athletes1 == parthner || u.Athletes2 == athletes || u.Athletes2 == parthner).Count() == 0)
                            {
                                int res = AppDbContext.db.Scorecards.ToList().Where(u => u.Category == categories).Count() + 1;
                                AppDbContext.db.Scorecards.Add(new Scorecards
                                {
                                    Athletes1 = athletes,
                                    Athletes2 = parthner,
                                    Category = categories,
                                    Trainer = athletes.Trainer,
                                    Number = res
                                });
                                await AppDbContext.db.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            return Unauthorized();
        }

        [Route("RegTourInfo")]
        [HttpGet]
        public ActionResult<List<string>> RegTourInfo()
        {
            int Id = UsersController.isValidJWT(Request);
            if (Id != -1)
            {
                Athletes athletes = AppDbContext.db.Athletes.FirstOrDefault(u => u.Id.Id == Id);
                return AppDbContext.db.Scorecards.ToList().Where(u => (u.Athletes1 == athletes || u.Athletes2 == athletes));
            }
            return Unauthorized();
        }

        public struct Tournament
        {
            public string name;
            public int id;
        }

        public struct Category
        {
            public string name;
            public int id;
        }

    }
}
