using System.Data.Entity;
using BookService.Models;

namespace BookService.DbContext
{
    public class BookContext : System.Data.Entity.DbContext
    {
        public BookContext() : base("name=BookService")
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
        public DbSet<Author> Authors {get;set;}
        public DbSet<Book> Books {get;set;}
    }
}