using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace DNDBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        private readonly DataContext _context;
        public GamesController(DataContext context)
        {
            this._context = context;

        }

        [HttpGet]
        // [Authorize]
        public async Task<ActionResult<IEnumerable<Game>>> Get()
        {
            var Games = await _context.Game.ToListAsync();
            return Ok(Games);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        // [Authorize]
        public async Task<ActionResult<Game>> Get(int id)
        {
            var Game = await _context.Game.FindAsync(id);
            return Ok(Game);
        }

        // POST api/users
        [HttpPost]
        public void Post([FromBody] string users)
        {
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string users)
        {
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
