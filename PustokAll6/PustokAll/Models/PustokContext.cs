using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokProje.Models
{
    public class PustokContext:DbContext
    {
        public PustokContext(DbContextOptions<PustokContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UpPromotion> UpPromotions { get; set; }
        public DbSet<DownPromotion> DownPromotions { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
}
