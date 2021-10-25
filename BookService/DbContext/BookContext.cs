using System.Data.Entity;
using BookService.Models;

namespace BookService.DbContext
{
    public class BookContext : System.Data.Entity.DbContext
    {
        public BookContext() : base("name=BookService")
        {
        }
        public DbSet<Author> Authors {get;set;}
        public DbSet<Book> Books {get;set;}
    }
}