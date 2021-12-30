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
    public class SettingController : Controller
    {
        private readonly PustokContext _context;

        public SettingController(PustokContext context)
        {
            this._context = context;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = (int)Math.Ceiling(Convert.ToDouble((_context.Settings.Count() / 2)));
            ViewBag.SelectedPage = page;
            return View(_context.Settings.Skip((page - 1) * 2).Take(2).ToList());
        }

        public IActionResult Edit(int id)
        {

            Settings settings = _context.Settings.FirstOrDefault(x => x.Id == id);

            if (settings == null) return NotFound();
            return View(settings);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Settings settings)
        {
            Settings existSetting = _context.Settings.FirstOrDefault(x => x.Id == settings.Id);
            if (existSetting == null) return NotFound();
            if (!ModelState.IsValid) return View();
            existSetting.Key = settings.Key;
            existSetting.Value = settings.Value;

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
