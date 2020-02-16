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
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var Users = await _context.Users.Include(u => u.Games).ToListAsync();
            return Ok(Users);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Get(int id)
        {
            var User = await _context.Users.FindAsync(id);
            return Ok(User);
        }
        // [HttpGet("{UserName}")]
        // [Authorize]
        // public async Task<ActionResult<User>> Get(string UserName)
        // {
        //     var User = await _context.Users.FindAsync(UserName);
        //     return Ok(User);
        // }

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
