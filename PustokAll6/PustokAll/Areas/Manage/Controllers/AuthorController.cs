using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokProje.Helper;
using PustokProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokProje.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AuthorController : Controller
    {
        private readonly PustokContext _context;
        private readonly IWebHostEnvironment _env;

        public AuthorController(PustokContext context , IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = (int)Math.Ceiling(Convert.ToDouble((_context.Authors.Count() / 2)));
            ViewBag.SelectedPage = page;
            return View(_context.Authors.Include(x => x.Books).Skip((page-1) * 2).Take(2).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {

            if (author.ImageFile == null) ModelState.AddModelError("ImageFile", "ImageFile is required!");

            else if (author.ImageFile.Length > 2097152) ModelState.AddModelError("ImageFile", "IMagefile max size is 2MB");

            else if (author.ImageFile.ContentType != "image/jpeg" && author.ImageFile.ContentType != "image/png") ModelState.AddModelError("ImageFile", "Type is incorrect!(jpg/png)");

            if (!ModelState.IsValid) return View();

            author.Image = FileManager.Save(_env.WebRootPath, "uploads/author", author.ImageFile);

            _context.Authors.Add(author);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {

            Author authors = _context.Authors.FirstOrDefault(x => x.Id==id);

            if (authors == null) return NotFound();
            return View(authors);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author author)
        {
            Author existAuthor = _context.Authors.FirstOrDefault(x => x.Id == author.Id);
            if (existAuthor == null) return NotFound();
            if (!ModelState.IsValid) return View();
            existAuthor.FullName = author.FullName;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == id);
            _context.Authors.Remove(author);
            _context.SaveChanges();

            return Ok();
        }
    }
}
