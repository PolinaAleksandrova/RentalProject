using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalProject.Data;
using RentalProject.Models;

namespace RentalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super user")]
    public class PremisesTypesController : Controller
    {
        private ApplicationDbContext _db;

        public PremisesTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            //var data = _db.PremisesTypes.ToList();
            return View(_db.PremisesTypes.ToList());
        }

        //GET Create Action Method
     
        public ActionResult Create()
        {
            return View();
        }

        //POST Create Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PremisesTypes premisesTypes)
        {
            if (ModelState.IsValid)
            {
                _db.PremisesTypes.Add(premisesTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Premises type has been saved";
                return RedirectToAction(nameof(Index));
            }

            return View(premisesTypes);
        }

        //GET Edit Action Method
   
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premisesTypes = _db.PremisesTypes.Find(id);
            if (premisesTypes == null)
            {
                return NotFound();
            }
            return View(premisesTypes);
        }

        //POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PremisesTypes premisesTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(premisesTypes);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Premises type has been updated";
                return RedirectToAction(nameof(Index));
            }

            return View(premisesTypes);
        }


        //GET Details Action Method

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premisesTypes = _db.PremisesTypes.Find(id);
            if (premisesTypes == null)
            {
                return NotFound();
            }
            return View(premisesTypes);
        }

        //POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(PremisesTypes premisesTypes)
        {
            return RedirectToAction(nameof(Index));

        }

        //GET Delete Action Method

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premisesTypes = _db.PremisesTypes.Find(id);
            if (premisesTypes == null)
            {
                return NotFound();
            }
            return View(premisesTypes);
        }

        //POST Delete Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, PremisesTypes premisesTypes)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != premisesTypes.Id)
            {
                return NotFound();
            }

            var premisesType = _db.PremisesTypes.Find(id);
            if (premisesType == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(premisesType);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Premises type has been deleted";
                return RedirectToAction(nameof(Index));
            }

            return View(premisesTypes);
        }

    }
}