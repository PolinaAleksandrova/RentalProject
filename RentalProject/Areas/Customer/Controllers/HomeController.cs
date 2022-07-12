using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalProject.Data;
using RentalProject.Models;
using RentalProject.Utility;
using X.PagedList;

namespace RentalProject.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index(int? page)
        {
            return View(_db.Premises.Include(c => c.PremisesTypes).Include(c => c.SpecialTag).ToList().ToPagedList(page ?? 1, 9));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET premises detail acation method

        public ActionResult Detail(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var room = _db.Premises.Include(c => c.PremisesTypes).FirstOrDefault(c => c.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        //POST premises detail acation method
        [HttpPost]
        [ActionName("Detail")]
        public ActionResult PremisesDetail(int? id)
        {
            List<Premises> premises = new List<Premises>();
            if (id == null)
            {
                return NotFound();
            }

            var room = _db.Premises.Include(c => c.PremisesTypes).FirstOrDefault(c => c.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            premises = HttpContext.Session.Get<List<Premises>>("premises");
            if (premises == null)
            {
                premises = new List<Premises>();
            }
            premises.Add(room);
            HttpContext.Session.Set("premises", premises);
            return RedirectToAction(nameof(Index));
        }
        //GET Remove action methdo
        [ActionName("Remove")]
        public IActionResult RemoveToSelection(int? id)
        {
            List<Premises> premises = HttpContext.Session.Get<List<Premises>>("premises");
            if (premises != null)
            {
                var room = premises.FirstOrDefault(c => c.Id == id);
                if (room != null)
                {
                    premises.Remove(room);
                    HttpContext.Session.Set("premises", premises);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public IActionResult Remove(int? id)
        {
            List<Premises> premises = HttpContext.Session.Get<List<Premises>>("premises");
            if (premises != null)
            {
                var room = premises.FirstOrDefault(c => c.Id == id);
                if (room != null)
                {
                    premises.Remove(room);
                    HttpContext.Session.Set("premises", premises);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //GET premises Selection action method

        public IActionResult Selection()
        {
            List<Premises> premises = HttpContext.Session.Get<List<Premises>>("premises");
            if (premises == null)
            {
                premises = new List<Premises>();
            }
            return View(premises);
        }

    }
}
