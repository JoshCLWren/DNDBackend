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
    public class UsersController : ControllerBase
    {

        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            this._context = context;

        }
// let's add the middleware for auth0 and try to access this endpoint
        // GET api/users
        [HttpGet]
        // [Route("private")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserTable>>> Get()
        {
            var UserTables = await _context.UserTables.ToListAsync();
            return Ok(UserTables);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserTable>> Get(int id)
        {
            var UserTable = await _context.UserTables.FindAsync(id);
            return Ok(UserTable);
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
