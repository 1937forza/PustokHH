using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokProje.Models
{
    public class DownPromotion:BaseEntity
    {
        public string Title { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string BtnText { get; set; }
        public string BgImage { get; set; }
    }
}
