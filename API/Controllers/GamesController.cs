using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;
using API;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.IdentityModel.Tokens.Jwt;

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
        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody] string users)
        {
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
        }
    }
}
