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

        // GET api/users
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        // if user is found in users.db retern ok else new user

        {   
            var Users = await _context.Users.Include(u => u.Games).ToListAsync();
            return Ok(Users);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Get(int id)
        {
            var client = new RestClient("https://dev-59tm9cah.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=EoGFS9Hp23cuIpTr5iiQSzJvJiYtGm3A%24%7Baccount.clientId%7D&client_secret=VymQ6EZjDo7J3nCIEaGGSuVG_W642JbzoLN6GEZkroRAV_1iCgOVA5FDtqRV1JiR&audience=https%3A%2F%2F%24%7Baccount.namespace%7D%2Fapi%2Fv2%2F", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var token = response.Content;
            Console.WriteLine(token);
            // why is this saying access denied? Are the parameters correct?
            var User = await _context.Users.FindAsync(id);
            Console.WriteLine(User); 
            
            return Ok(User);

        }


        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new {UserName = user.UserName, email = user.Email}, user);
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
