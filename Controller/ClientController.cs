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

        private readonly IMongoCollection<Client> _client;
        private readonly IMongoCollection<User> _user;

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
        public async Task<ActionResult<Client>> GetById(string id)
        {
            try
            {
                var clientFinded = await _client.Find(c => c.Id == id).FirstOrDefaultAsync();
                if (clientFinded == null)
                {
                    return NotFound();
                }
                return Ok(clientFinded);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Client novoClient)
        {
            try
            {
                
                var userFinded = await _user.Find(u => u.Id == novoClient.UserId).FirstOrDefaultAsync();

                if (userFinded == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                novoClient.User = userFinded; 
                await _client.InsertOneAsync(novoClient);

                return CreatedAtAction(nameof(GetById), new { id = novoClient.Id }, novoClient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] Client atualizado)
        {
            try
            {
                
                var existingClient = await _client.Find(c => c.Id == id).FirstOrDefaultAsync();
                if (existingClient == null)
                {
                    return NotFound("Cliente não encontrado");
                }

                var filter = Builders<Client>.Filter.Eq(c => c.Id, id);
                await _client.ReplaceOneAsync(filter, atualizado);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

      
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var deleteResult = await _client.DeleteOneAsync(c => c.Id == id);

                if (deleteResult.DeletedCount == 0)
                {
                    return NotFound("Cliente não encontrado");
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
