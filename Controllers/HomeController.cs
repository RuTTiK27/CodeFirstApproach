using CodeFirstApproach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CodeFirstApproach.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly StdentDBContext studentDB;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController(StdentDBContext studentDB)
        {
            this.studentDB = studentDB;
        }

        public async Task<IActionResult>  Index()
        {
            var studentData = await studentDB.Students.ToListAsync();
            return View(studentData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student std)
        {
            if (ModelState.IsValid)
            {
                await studentDB.Students.AddAsync(std);
                await studentDB.SaveChangesAsync();
                TempData["insert_success"] = "Insrted...";
                return RedirectToAction("Index","Home");
            }
            return View(std);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var studentData = await studentDB.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (studentData == null)
            {
                return NotFound();
            }
            return View(studentData);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var studentData = await studentDB.Students.FindAsync(id);
            if (studentData == null)
            {
                return NotFound();
            }
            return View(studentData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Student std) 
        {
            if (id!=std.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                studentDB.Update(std);
                await studentDB.SaveChangesAsync();
                TempData["update_success"] = "Updated...";
                return RedirectToAction("Index", "Home");
            }
            return View(std);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var studentData = await studentDB.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (studentData == null)
            {
                return NotFound();
            }
            return View(studentData);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var studentData = await studentDB.Students.FindAsync(id);
            if (studentData != null)
            {
                studentDB.Students.Remove(studentData);
                await studentDB.SaveChangesAsync();
                TempData["delete_success"] = "Deleted...";
            }
            return RedirectToAction("Index", "Home");
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
    }
}
