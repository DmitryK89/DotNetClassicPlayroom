using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BookService.DbContext;
using BookService.Models.Dto;
using BookService.Models.Entities;

namespace BookService.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        private BookContext _db;

        public BooksController(BookContext db)
        {
            _db = db;
        }


        /// <summary>
        /// GET: api/Books
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public IQueryable<BookDto> GetBooks()
        {
            return _db.Books
                .Include(b=>b.Author)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.Name
                });
        }

        /// <summary>
        /// GET: api/Books/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var book = await _db.Books
                .Select(AsBookDto())
                .FirstOrDefaultAsync(b => b.Id == id);
                
            if (book == null)
            {
                return NotFound();
            }
        
            return Ok(book);
        }

        /// <summary>
        /// GET: api/Books/5/details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}/details")]
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> GetBookDetail(int id)
        {
            var book = await _db.Books
                .Select(AsBookDetailDto())
                .FirstOrDefaultAsync(b => b.Id == id);
                
            if (book == null)
            {
                return NotFound();
            }
        
            return Ok(book);
        }

        /// <summary>
        /// GET: api/Books/fantasy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{genre}")]
        [ResponseType(typeof(Book))]
        public IQueryable<BookDto> GetBookGenre(string genre)
        {
            var book = _db.Books
                .Where(b=> b.Genre.Equals(genre, StringComparison.InvariantCultureIgnoreCase))
                .Select(AsBookDto());
            return book;
        }
        
        /// <summary>
        /// GET: /api/authors/id/books
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("~/api/authors/{authorId:int}/books")]
        [ResponseType(typeof(Book))]
        public IQueryable<BookDto> GetBookByAuthors(int authorId)
        {
            var book = _db.Books
                .Where(b=> b.Author.Id==authorId)
                .Select(AsBookDto());
            return book;
        }

        /// <summary>
        /// PUT: api/Books/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            _db.Entry(book).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// POST: api/Books
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Books.Add(book);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        /// <summary>
        /// DELETE: api/Books/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = _db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            _db.Books.Remove(book);
            _db.SaveChanges();

            return Ok(book);
        }

        private bool BookExists(int id)
        {
            return _db.Books.Count(e => e.Id == id) > 0;
        }
        
        private static Expression<Func<Book, BookDto>> AsBookDto()
        {
            return b=>new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                AuthorName = b.Author.Name,
            };
        }
        
        private static Expression<Func<Book, BookDetailDto>> AsBookDetailDto()
        {
            return b => new BookDetailDto
            {
                Id = b.Id,
                Title = b.Title,
                Year = b.Year,
                Price = b.Price,
                AuthorName = b.Author.Name,
                Genre = b.Genre
            };
        }
    }
}