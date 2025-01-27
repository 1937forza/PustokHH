﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokProje.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class GenreController : Controller
    {
        private readonly PustokContext _context;

        public GenreController(PustokContext context)
        {
            this._context = context;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = (int)Math.Ceiling(Convert.ToDouble((_context.Genres.Count() / 2)));
            ViewBag.SelectedPage = page;
            return View(_context.Genres.Include(x => x.Books).Skip((page - 1) * 2).Take(2).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null) return NotFound();

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Genre genre)
        {
            Genre existsGenre = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);

            if (existsGenre == null) return NotFound();
            if (!ModelState.IsValid) return View();

            existsGenre.Name = genre.Name;
            _context.SaveChanges();


            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == id);
            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return Ok();
        }
    }
}
