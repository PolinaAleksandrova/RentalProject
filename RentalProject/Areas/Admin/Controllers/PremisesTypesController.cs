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

    }
}
