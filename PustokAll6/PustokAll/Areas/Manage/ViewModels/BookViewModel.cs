using PustokProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokProje.Areas.Manage.ViewModels
{
    public class BookViewModel
    {
        public List<Author> Authors { get; set; }
        public List<Genre> Genres { get; set; }
        public Book Book { get; set; }
    }
}
