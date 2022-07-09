using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalProject.Data;
using RentalProject.Models;

namespace RentalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PremisesController : Controller
    {
        private ApplicationDbContext _db;
        private IHostingEnvironment _he;

        public PremisesController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.Premises.Include(c => c.PremisesTypes).Include(f => f.SpecialTag).ToList());
        }

        //POST Index action method
        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var premises = _db.Premises.Include(c => c.PremisesTypes).Include(c => c.SpecialTag)
                .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if (lowAmount == null || largeAmount == null)
            {
                premises = _db.Premises.Include(c => c.PremisesTypes).Include(c => c.SpecialTag).ToList();
            }
            return View(premises);
        }

        //Get Create method
        public IActionResult Create()
        {
            ViewData["premisesTypeId"] = new SelectList(_db.PremisesTypes.ToList(), "Id", "PremisesType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
            return View();
        }


        //Post Create method
        [HttpPost]
        public async Task<IActionResult> Create(Premises premises, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchPremises = _db.Premises.FirstOrDefault(c => c.Name == premises.Name);
                if (searchPremises != null)
                {
                    ViewBag.message = "This premises is already exist";
                    ViewData["premisesTypeId"] = new SelectList(_db.PremisesTypes.ToList(), "Id", "PremisesType");
                    ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
                    return View(premises);
                }

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    premises.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    premises.Image = "Images/noimage.PNG";
                }
                _db.Premises.Add(premises);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(premises);
        }

        //GET Edit Action Method

        public ActionResult Edit(int? id)
        {
            ViewData["premisesTypeId"] = new SelectList(_db.PremisesTypes.ToList(), "Id", "PremisesType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }

            var premises = _db.Premises.Include(c => c.PremisesTypes).Include(c => c.SpecialTag)
                .FirstOrDefault(c => c.Id == id);
            if (premises == null)
            {
                return NotFound();
            }
            return View(premises);
        }

        //POST Edit Action Method
        [HttpPost]
        public async Task<IActionResult> Edit(Premises premises, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    premises.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    premises.Image = "Images/noimage.PNG";
                }
                _db.Premises.Update(premises);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(premises);
        }

        //GET Details Action Method
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var premises = _db.Premises.Include(c => c.PremisesTypes).Include(c => c.SpecialTag)
                .FirstOrDefault(c => c.Id == id);
            if (premises == null)
            {
                return NotFound();
            }
            return View(premises);
        }

        //GET Delete Action Method

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premises = _db.Premises.Include(c => c.SpecialTag).Include(c => c.PremisesTypes).Where(c => c.Id == id).FirstOrDefault();
            if (premises == null)
            {
                return NotFound();
            }
            return View(premises);
        }

        //POST Delete Action Method

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premises = _db.Premises.FirstOrDefault(c => c.Id == id);
            if (premises == null)
            {
                return NotFound();
            }

            _db.Premises.Remove(premises);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}