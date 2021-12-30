using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PustokProje.Models;
using PustokProje.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PustokProje.Areas.Manage.Controllers
{

    [Area("manage")]

    public class SliderController : Controller
    {
        private readonly PustokContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(PustokContext context , IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.TotalPage = (int)Math.Ceiling(Convert.ToDouble((_context.Sliders.Count() / 2)));
            ViewBag.SelectedPage = page;
            return View(_context.Sliders.ToList());
        }

        public IActionResult Create()
        { 

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (slider.Title1 == slider.Title2)
                ModelState.AddModelError("Title2", "Title2 must not be same as Title1");

            if (slider.ImageFile == null)
                ModelState.AddModelError("ImageFile", "ImageFile is required");
            else if (slider.ImageFile.Length > 2097152)
                ModelState.AddModelError("ImageFile", "ImageFile max size is 2MB");
            else if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                ModelState.AddModelError("ImageFile", "ContentType must be image/jpeg or image/png");

            if (!ModelState.IsValid) return View();

            slider.Image = FileManager.Save(_env.WebRootPath, "uploads/slider", slider.ImageFile);

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);

            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            Slider existsSlider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (existsSlider == null) return NotFound();

            existsSlider.Title1 = slider.Title1;
            existsSlider.Title2 = slider.Title2;
            existsSlider.Desc = slider.Desc;
            existsSlider.BtnText = slider.BtnText;
            existsSlider.Order = slider.Order;
            existsSlider.RedirectUrl = slider.RedirectUrl;

            if (slider.ImageFile != null)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/slider", existsSlider.Image);

                existsSlider.Image = FileManager.Save(_env.WebRootPath, "uploads/slider", slider.ImageFile);
            }

            _context.SaveChanges();

            return RedirectToAction("index");
        }


    }
}
