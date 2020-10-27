using HelloWebMvcEF.Data;
using HelloWebMvcEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWebMvcEF.Controllers
{
    public class GradoviController : Controller
    {
        private readonly ProbaContext _context;

        public GradoviController(ProbaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(_context.Grad.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grad = await _context.Grad
                .Include(g => g.Skole)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (grad == null)
            {
                return NotFound();
            }

            return View(grad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        [Bind("Id", "Naziv")] Grad grad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(grad);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Greška pri dodavanju grada. " +
                    "Pokušajte ponovo, a ako problem " +
                    "ne bude rešen zovite administratora sistema.");
            }
            return View(grad);
        }
    }
}