using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiddleWebApp.Data;
using RiddleWebApp.Dtos;
using RiddleWebApp.Models;
using RiddleWebApp.Services;

namespace RiddleWebApp.Controllers
{
    public class RiddleController : Controller
    {
        private readonly ApplicationDbContext context;

        private readonly IRiddleService riddleService;

        public RiddleController(ApplicationDbContext context, IRiddleService riddleService)
        {
            this.riddleService = riddleService;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var riddles = riddleService.GetAllRiddles();
            return View(riddles);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Question, Answer")] RiddleDto riddleDto )
        {
            if (ModelState.IsValid)
            {
                riddleService.AddRiddle(riddleDto);
                return RedirectToAction(nameof(Index));
            }

            return View(riddleDto);
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
