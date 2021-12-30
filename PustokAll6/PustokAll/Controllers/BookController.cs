using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokProje.Models;
using PustokProje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PustokProje.Controllers
{
    public class BookController : Controller
    {
        private readonly PustokContext _context;

        public BookController(PustokContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> AddBasketAsync(int bookId)

        {
            if (!_context.Books.Any(x => x.Id == bookId))
            {
                return NotFound();
            }


            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();
            string existBasketItems = HttpContext.Request.Cookies["ItemList"];

            if (existBasketItems != null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(existBasketItems);
            }

            BasketItemViewModel item = basketItems.FirstOrDefault(x => x.BookId == bookId);

            if (item == null)
            {
                item = new BasketItemViewModel
                {
                    BookId = bookId,
                    Count = 1
                };
                basketItems.Add(item);
            }
            else
            {
                item.Count++;
            }


            var bookIdStr = JsonConvert.SerializeObject(basketItems);

            HttpContext.Response.Cookies.Append("ItemList", bookIdStr);


            List<BasketBookViewModel> books = new List<BasketBookViewModel>();

            foreach (var basketItem in basketItems)
            {
                var book = await _context.Books
                        .Include(b => b.BookImages)
                        .Where(b => b.Id == basketItem.BookId)
                        .FirstOrDefaultAsync();

                var basketBook = new BasketBookViewModel
                {
                    Id = book.Id,
                    Name = book.Name,
                    Image = book.BookImages.Where(b => b.PosterStatus == true).FirstOrDefault().Image,
                    Price = book.SalePrice,
                    Count = basketItem.Count
                };

                books.Add(basketBook);
            }

            return Json(books);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBasketAsync(int bookId)
        {
            if (!_context.Books.Any(x => x.Id == bookId))
            {
                return NotFound();
            }


            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();
            string existBasketItems = HttpContext.Request.Cookies["ItemList"];

            if (existBasketItems != null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(existBasketItems);
            }

            BasketItemViewModel item = basketItems.FirstOrDefault(x => x.BookId == bookId);

            basketItems.Remove(item);

            var bookIdStr = JsonConvert.SerializeObject(basketItems);

            HttpContext.Response.Cookies.Append("ItemList", bookIdStr);

            List<BasketBookViewModel> books = new List<BasketBookViewModel>();

            foreach (var basketItem in basketItems)
            {
                var book = await _context.Books
                        .Include(b => b.BookImages)
                        .Where(b => b.Id == basketItem.BookId)
                        .FirstOrDefaultAsync();
                
                var basketBook = new BasketBookViewModel
                {
                    Id = book.Id,
                    Name = book.Name,
                    Image = book.BookImages.Where(b => b.PosterStatus == true).FirstOrDefault().Image,
                    Price = book.SalePrice,
                    Count = basketItem.Count
                };

                books.Add(basketBook);
            }

            return Json(books);
        }

        public IActionResult ShowBasket()
        {
            var bookIdStr = HttpContext.Request.Cookies["ItemList"];
            List<BasketItemViewModel> bookIds = new List<BasketItemViewModel>();
            if (bookIdStr != null)
            {
                bookIds = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(bookIdStr);
            }

            return Json(bookIds);
        }

        public IActionResult Checkout()
        {
            List<CheckOutViewModel> checkoutItems = new List<CheckOutViewModel>();

            string basketItemsStr = HttpContext.Request.Cookies["ItemList"];
            if (basketItemsStr != null)
            {
                List<BasketItemViewModel> basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);

                foreach (var item in basketItems)
                {
                    CheckOutViewModel checkoutItem = new CheckOutViewModel
                    {
                        Book = _context.Books.FirstOrDefault(x => x.Id == item.BookId),
                        Count = item.Count
                    };
                    checkoutItems.Add(checkoutItem);
                }
            }

            return View(checkoutItems);
        }
    }
}
