using Microsoft.AspNetCore.Mvc;
using RentalProject.Data;
using RentalProject.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RentalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PremisesTypesController : Controller
    {
        private ApplicationDbContext _db;

        public PremisesTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var data = _db.PremisesTypes.ToList();
            return View(_db.PremisesTypes.ToList());
        }

        //Створення GET Action Method

        public ActionResult Create()
        {
            return View();
        }

        //Створення POST Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PremisesTypes premisesTypes)
        {
            if (ModelState.IsValid)
            {
                _db.PremisesTypes.Add(premisesTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product type has been saved";
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

            var productType = _db.PremisesTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
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
                TempData["edit"] = "Product type has been updated";
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

            var premisesType = _db.PremisesTypes.Find(id);
            if (premisesType == null)
            {
                return NotFound();
            }
            return View(premisesType);
        }

        //POST Details Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(PremisesTypes premisesType)
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

            var productType = _db.PremisesTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
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

            var productType = _db.PremisesTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(premisesTypes);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Product type has been deleted";
                return RedirectToAction(nameof(Index));
            }

            return View(premisesTypes);
        }
    }
}
