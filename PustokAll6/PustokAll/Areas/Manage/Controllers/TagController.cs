using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokProje.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TagController : Controller
    {
        private readonly PustokContext _context;

        public TagController(PustokContext context)
        {
            this._context = context;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = (int)Math.Ceiling(Convert.ToDouble((_context.Tags.Count() / 2)));
            ViewBag.SelectedPage = page;
            return View(_context.Tags.Include(x => x.BookTags).Skip((page - 1) * 2).Take(2).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Tag tag = _context.Tags.FirstOrDefault(x => x.Id == id);
            if (tag == null) return NotFound();

            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Tag tag)
        {
            Tag existsTag = _context.Tags.FirstOrDefault(x => x.Id == tag.Id);

            if (existsTag == null) return NotFound();
            if (!ModelState.IsValid) return View();

            existsTag.Name = tag.Name;
            _context.SaveChanges();


            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var tag = _context.Tags.FirstOrDefault(x => x.Id == id);
            _context.Tags.Remove(tag);
            _context.SaveChanges();

            return Ok();
        }
    }
}
