using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using RentalProject.Data;

namespace RentalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PremisesController : Controller
    {
        private ApplicationDbContext _db;


        public PremisesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Premises.Include(c => c.PremisesTypes).Include(f => f.SpecialTag).ToList());
        }
    }
}
