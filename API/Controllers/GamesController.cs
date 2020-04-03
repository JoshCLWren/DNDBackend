using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Microsoft.AspNetCore.Http;

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
        [Authorize]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            var Games = await _context.Game.ToListAsync();
            return Ok(Games);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Game>> Get(Guid Id)
        {
            var Game = await _context.Game.FindAsync(Id);
            return Ok(Game);
        }

        // POST api/users
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostGame(Game game)
        {
            _context.Game.Add(game);
            await _context.SaveChangesAsync();
 
            return CreatedAtAction("GetGames", new {GameId = Guid.NewGuid()}, game );
        }
        // PUT api/games/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult PUT (Guid id, [FromBody] Game game)
        {
            var dbGame = _context.Game
                .FirstOrDefault(s => s.GameId.Equals(id));
 
            dbGame.GameName = game.GameName;
            dbGame.UserId = game.UserId;
 
            _context.SaveChanges();

 
            return NoContent();
        }
 
        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
        }
        private bool GameExists(Guid id)
        {
            return _context.Game.Any(e => e.GameId == id);
        }
    }
}
