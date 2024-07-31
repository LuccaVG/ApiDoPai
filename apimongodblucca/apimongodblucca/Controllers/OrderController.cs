using apimongodblucca.Domains;
using apimongodblucca.Services;
using apimongodblucca.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace apimongodblucca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _order;
        private readonly IMongoCollection<Client> _client;
        private readonly IMongoCollection<Product> _product;
        private readonly IMongoCollection<User> _user;

        public OrderController(MongoDbService mongoDbService)
        {
            _order = mongoDbService.GetDatabase.GetCollection<Order>("Pedidos");
            _client = mongoDbService.GetDatabase.GetCollection<Client>("Clientes");
            _product = mongoDbService.GetDatabase.GetCollection<Product>("Produtos");
            _user = mongoDbService.GetDatabase.GetCollection<User>("Usuarios");
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            try
            {
                var orders = await _order.Find(FilterDefinition<Order>.Empty).ToListAsync();

                foreach (var order in orders)
                {
                    if (order.ProductId != null)
                    {
                        var filter = Builders<Product>.Filter.In(p => p.Id, order.ProductId);

                        order.Products = await _product.Find(filter).ToListAsync();
                    }

                    if (order.ClientId != null)
                    {
                        order.Client = await _client.Find(x => x.Id == order.ClientId).FirstOrDefaultAsync();
                    }
                }

                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Order>> Create(OrderViewModel orderViewModel)
        {
            try
            {
            Order order = new Order();

            order.Id = orderViewModel.Id;
            order.Date = orderViewModel.Date;
            order.Status = orderViewModel.Status;
            order.Products = orderViewModel.Products;
            order.ProductId = orderViewModel.ProductId;
            order.ClientId = orderViewModel.ClientId;

            var client = await _client.Find(x => x.Id == order.ClientId).FirstOrDefaultAsync();

            if (client == null)
            {
                return NotFound("Cliente nao existe");
            }

            order.Client = client;

            await _order!.InsertOneAsync(order);

                return StatusCode(201, order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(string id)
        {
            try
            {
                //var order = await _order.Find(x => x.Id == id).FirstOrDefaultAsync();
                //var filter = Builders<order>.Filter.Eq(x => x.Id, id);
                try
                {
                    var orders = await _order.Find(x => x.Id == id).ToListAsync();

                    foreach (var order in orders)
                    {
                        if (order.ProductId != null)
                        {
                            var filter = Builders<Product>.Filter.In(p => p.Id, order.ProductId);

                            order.Products = await _product.Find(filter).ToListAsync();
                        }

                        if (order.ClientId != null)
                        {
                            order.Client = await _client.Find(x => x.Id == order.ClientId).FirstOrDefaultAsync();
                        }
                    }

                    return Ok(orders);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

                //return Ok(order);
                //return Ok(filter);    
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update(Order o)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, o.Id);
            try
            {
                await _order.ReplaceOneAsync(filter, o);

                return Ok();
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
                var filter = Builders<Order>.Filter.Eq(x => x.Id, id);

                if (filter != null)
                {
                    await _order.DeleteOneAsync(filter);

                    return Ok();
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
