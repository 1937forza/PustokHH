using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokProje.Areas.Manage.ViewModels;
using PustokProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokProje.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookController : Controller
    {

        

        private readonly PustokContext _context;

        public BookController(PustokContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Books.Include(x => x.BookImages).Include(x => x.BookTags).Include(x => x.Genre).Include(x => x.Author).ToList());
        }

        //public IActionResult Create()
        //{
        //    BookViewModel bookVM = new BookViewModel
        //    {
        //        Genres = _context.Genres.ToList(),
        //        Authors = _context.Authors.ToList(),
        //    };

        //    return View(bookVM);

        //}

        [HttpPost]
        public IActionResult Create(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
