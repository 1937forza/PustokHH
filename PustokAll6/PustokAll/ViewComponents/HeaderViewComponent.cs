using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokProje.ViewModels;
using PustokProje.Models;
using PustokProje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokProje.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly PustokContext _context;

        public HeaderViewComponent(PustokContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderViewModel headerVM = new HeaderViewModel
            {
                Genres = await _context.Genres.ToListAsync(),
                Settings = await _context.Settings.ToListAsync()
            };
            return View(headerVM);
        }
    }
}
