using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIMongoDB.Domains;
using MinimalAPIMongoDB.Services;
using MongoDB.Driver;

namespace MinimalAPIMongoDB.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
   
        private readonly IMongoCollection<Client>? _client;
        private readonly IMongoCollection<User>? _user;

        public ClientController(MongoDbService mongoDbService)
        {
            _client = mongoDbService.GetDatabase.GetCollection<Client>("Client");
            _user = mongoDbService.GetDatabase.GetCollection<User>("User");
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get()
        {
            try
            {
                var clients = await _client.Find(FilterDefinition<Client>.Empty).ToListAsync();
                return Ok(clients);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            try
            {
                var clientFinded = await _client.Find(c => c.Id == id).FirstOrDefaultAsync();

                return clientFinded is not null ? Ok(clientFinded) : NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Client novoClient)
        {
            try
            {
                var userFinded = await _user.Find(u => u.Id == novoClient.UserId).FirstOrDefaultAsync();

                if (userFinded is not null)
                {
                    novoClient.user = userFinded;
                    await _client.InsertOneAsync(novoClient);
                }
                else
                {
                    return NotFound("Usuario nao encontrado");
                }

                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(Client atualizado)
        {
            try
            {
                var filter = Builders<Client>.Filter.Eq(c => c.Id, atualizado.Id);

                await _client.ReplaceOneAsync(filter, atualizado);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _client.FindOneAndDeleteAsync(c => c.Id == id);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
