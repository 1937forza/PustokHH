using Microsoft.EntityFrameworkCore;
using PustokProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokProje.Services
{
    public class LayoutService
    {
        private readonly PustokContext _context;

        public LayoutService(PustokContext context)
        {
            this._context = context;
        }

        public async Task<List<Settings>> GetSettings()
        {
            return await _context.Settings.ToListAsync();
        }
    }
}
