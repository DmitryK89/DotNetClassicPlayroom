using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.OData;
using ProductService.Context;
using ProductService.Models;

namespace ProductService.Controllers
{
    public class SuppliersController : ODataController
    {
        private readonly ProductContext _db = new ProductContext();

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
        
        [EnableQuery]
        public IQueryable<Supplier> Get()
        {
            return _db.Suppliers;
        }

        [EnableQuery]
        public SingleResult<Supplier> Get([FromODataUri] int key)
        {
            var result = _db.Suppliers.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

        
        // GET /Suppliers(1)/Products
        [EnableQuery]
        public IQueryable<Product> GetProducts([FromODataUri] int key)
        {
            return _db.Suppliers.Where(s => s.Id == key).SelectMany(s => s.Products);
        }
    }
}