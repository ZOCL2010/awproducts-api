using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using adventure_forks_database;
using Swashbuckle.Swagger.Annotations;

namespace adventure_forks_api.Controllers
{
    [Route("products")]
    public class ProductController : ApiController
    {
        private readonly IDatabaseService _databaseService;

        public ProductController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // GET api/products
        [SwaggerOperation("Gets all documents.")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult Get()
        {
            var allProducts = _databaseService.GetAllProducts();
            return Ok(allProducts);
        }

        // GET api/products/{id}
        [SwaggerOperation("Gets document by document Id.")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("products/{id}")]
        public IHttpActionResult Get(int id)
        {
            var product = _databaseService.GetProduct(id);
            if (product == null)
            {
                return NotFound();

            }

            return Ok(product);
        }

        // POST api/products
        [SwaggerOperation("Creates new document.")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public IHttpActionResult Post([FromUri] string name, 
                                      [FromUri] string productNumber, 
                                      [FromUri] decimal standardCost,
                                      [FromUri] decimal listPrice, 
                                      [FromUri] string size, 
                                      [FromUri] decimal weight)
        {
            var newProduct = _databaseService.CreateProduct(name, productNumber, standardCost, listPrice, size, weight);
            return Created($"/products/{newProduct.ProductID}", newProduct);
        }

        // PUT api/products/{id}
        [SwaggerOperation("Updates document.")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Put([FromUri] int productId, 
                                     [FromUri] string name,
                                     [FromUri] string productNumber,
                                     [FromUri] decimal standardCost,
                                     [FromUri] decimal listPrice,
                                     [FromUri] string size,
                                     [FromUri] decimal weight)
        {
            var result = _databaseService.UpdateProduct(productId, name, productNumber, standardCost, listPrice, size, weight);
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        // DELETE api/products/{id}
        [SwaggerOperation("Removes document by document Id.")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("products/{id}")]
        public IHttpActionResult Delete(int productId)
        {
            var result = _databaseService.DeleteProduct(productId);
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
