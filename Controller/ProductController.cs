using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIMongoDB.Domains;
using MinimalAPIMongoDB.Services;
using MongoDB.Driver;

namespace MinimalAPIMongoDB.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMongoCollection<Product> _product;

        public ProductController(MongoDbService mongoDbService)
        {
            _product = mongoDbService.GetDatabase.GetCollection<Product>("Product");
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                var products = await _product.Find(FilterDefinition<Product>.Empty).ToListAsync();

                return Ok(products);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            try
            {
                await _product.InsertOneAsync(product);
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Put(string id, [FromBody] Product product)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq("Id", id);
                var updateResult = await _product.ReplaceOneAsync(filter, product);

                if (updateResult.MatchedCount == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq("Id", id);
                var deleteResult = await _product.DeleteOneAsync(filter);

                if (deleteResult.DeletedCount == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
