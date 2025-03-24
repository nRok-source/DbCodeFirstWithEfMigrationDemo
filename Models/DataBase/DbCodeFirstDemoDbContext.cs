using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbCodeFirstDemo.Models.DataBase
{
    public partial class DbCodeFirstDemoDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var _configurationRoot = configurationBuilder.Build();
            optionsBuilder.UseSqlServer(_configurationRoot.GetConnectionString("DemoCodeFirst"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().Property(b => b.Creation).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
            modelBuilder.Entity<Blog>().Property(b => b.MAJ).HasDefaultValueSql("getdate()").ValueGeneratedOnUpdate();
        }
    }

    [Table("Blogs")]
    public class Blog
    {
        public int Id { get; set; }
         public string? Url { get; set; }
        public string? Html { get; set; }
        public DateTimeOffset Creation { get; set; }
        public DateTimeOffset MAJ { get; set; }
        [InverseProperty("Blog")]
        public virtual ICollection<Page> Pages { get; set; } = new List<Page>();
    }

    [Table("Page")]
    public class Page
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? Title { get; set; }
        public string? Html { get; set; }
        
        public int BlogId { get; set; }
        public DateTimeOffset Creation { get; set; }
        public DateTimeOffset? MAJ { get; set; }
        [ForeignKey("BlogId")]
        [InverseProperty("Pages")]
        public virtual Blog Blog { get; set; }

        [InverseProperty("Page")]
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }

    [Table("Comment")]
    public class Comment
    {
        public int Id { get; set; }

        public string? Url { get; set; }
        public string? Title { get; set; }
        //public string? Text { get; set; }
        public string? Author { get; set; }
        public string? Html { get;set; }
        
        public int PageId { get; set; }
        public DateTimeOffset Creation { get; set; }
        public DateTimeOffset? MAJ { get; set; }

        [ForeignKey("PageId")]
        [InverseProperty("Comments")]
        public virtual Page Page { get; set; }

    }


}
