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
    public class FeatureController : Controller
    {

        private readonly PustokContext _context;

        public FeatureController(PustokContext context)
        {
            this._context = context;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.TotalPage = (int)Math.Ceiling(Convert.ToDouble((_context.Features.Count() / 2)));
            ViewBag.SelectedPage = page;
            return View(_context.Features.Skip((page - 1) * 2).Take(2).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Feature feature)
        {
            _context.Features.Add(feature);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Feature feature = _context.Features.FirstOrDefault(x => x.Id == id);
            if (feature == null) return NotFound();

            return View(feature);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Feature feature)
        {
            Feature existsFeature = _context.Features.FirstOrDefault(x => x.Id == feature.Id);

            if (existsFeature == null) return NotFound();
            if (!ModelState.IsValid) return View();

            existsFeature.Title = feature.Title;
            existsFeature.Text = feature.Text;
            existsFeature.Icon = feature.Icon;
            _context.SaveChanges();


            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var feature = _context.Features.FirstOrDefault(x => x.Id == id);
            _context.Features.Remove(feature);
            _context.SaveChanges();

            return Ok();
        }
    }
}
