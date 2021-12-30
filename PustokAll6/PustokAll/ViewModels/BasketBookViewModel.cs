using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokProje.ViewModels
{
    public class BasketBookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
    }
}
