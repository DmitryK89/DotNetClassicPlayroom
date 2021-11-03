using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using ProductServiceCrud.Models;
using ProductServiceCrud.Repository;

public class ProductsController : ApiController
{
    public IEnumerable<Product> GetAllProducts()
    {
        return ProductRepository.Products;
    }

    public IHttpActionResult GetProduct(int id)
    {
        var product = ProductRepository.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    
    public IHttpActionResult DeleteProduct(int id)
    {
        var product = ProductRepository.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        ProductRepository.Products.Remove(product);
        
        return Ok(product);
    }

    [ResponseType(typeof(Product))]
    public IHttpActionResult PostProduct(Product product)
    {
        if (ModelState.IsValid)
        {
            if (ProductRepository.Products.Any(p => p.Id == product.Id))
            {
                return BadRequest("Id is busy");
            }
            ProductRepository.Products.Add(product);
            return Created("DefaultApi",product);
        }
        return BadRequest();
    }
    
    
    [ResponseType(typeof(Product))]
    public IHttpActionResult PutProduct(Product newProduct)
    {
        if (ModelState.IsValid)
        {
            var oldProduct = ProductRepository.Products.FirstOrDefault(p => p.Id == newProduct.Id);
            if (oldProduct==null)
            {
                return NotFound();
            }
            
            ProductRepository.Products.Remove(oldProduct);
            ProductRepository.Products.Add(newProduct);
            
            return Ok(newProduct);
        }
        return BadRequest();
    }
}