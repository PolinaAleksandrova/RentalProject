using Microsoft.AspNetCore.Mvc;
using RentalProject.Data;
using System.Linq;

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
            var data = _db.PremisesTypes.ToList();
            return View(_db.PremisesTypes.ToList());
        }
    }
}
