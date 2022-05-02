
using kinder_app.Data;
using kinder_app.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kinder_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageAPIController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public MessageAPIController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<MessageAPIController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MessageAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MessageAPIController>
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(string json)
        {
            Console.WriteLine("Dejimas");
            NameValueCollection data = JsonConvert.DeserializeObject<NameValueCollection>(json);
            var mess = new Message((data["sender"]), (data["receiver"]), data["text"]);
            if (String.IsNullOrWhiteSpace(mess.Text))
                return BadRequest();
            _context.Add(mess);
            await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetUser", new { id = user.Id }, user);

            return BadRequest();
        }

        // PUT api/<MessageAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MessageAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
