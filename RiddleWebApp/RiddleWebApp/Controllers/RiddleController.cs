using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiddleWebApp.Data;
using RiddleWebApp.Models;

namespace RiddleWebApp.Controllers
{
    public class RiddleController : Controller
    {
        private readonly ApplicationDbContext context;

        public RiddleController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Riddle.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Question, Answer")] Riddle riddle )
        {
            if (ModelState.IsValid)
            {
                context.Add(riddle);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(riddle);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var riddle = await context.Riddle.FirstOrDefaultAsync( m => m.Id == id);

            if (riddle == null)
            {
                return NotFound();
            }

            return View(riddle);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var riddle = await context.Riddle.FirstOrDefaultAsync(m => m.Id == id);

            if (riddle == null)
            {
                return NotFound();
            }

            return View(riddle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var riddle = await context.Riddle.FindAsync(id);
            context.Riddle.Remove(riddle);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
