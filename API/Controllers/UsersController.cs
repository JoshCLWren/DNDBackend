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
        // if user is found in users.db return ok else new user
        
        {
            // gets a token to use to look up user using auth0 v2 mgmt console 
            // AuthV2Token authV2Token = new AuthV2Token();
            // await AuthV2Token.ClientCredentialsFlow();
            // await AuthV2Token.GetUsers();
            // if user is in auth0 tenant display user else post user

            var Users = await _context.Users.Include(u => u.Games).ToListAsync();
            System.Console.WriteLine(Users);
            return Ok(Users);
        }
        
        // GET api/users/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Get(long id, User user)
        {
            // take header and save to responseHeader var
            var responseHeader = Request.Headers["Authorization"].FirstOrDefault();
            // instantiate UserIDCLass which will find value of sub which will be used as UserID
            UserIDClass useridclass = new UserIDClass();
            //pass the responseheader and id to the method to regex out the id in the sub value of the user object in responseHeader
            useridclass.idMethod(responseHeader, id);
            //id is saved to User var
            var User = await _context.Users.FindAsync(id);  
            //goodId is the regexed id from the responseHeader
            var goodId = useridclass.idMethod(responseHeader, id);  
            //https://docs.microsoft.com/en-us/ef/ef6/querying/ 
            // don't call your api inside your api
            // if (id = an id in database)
                //return ok user
                //else (add user)
            var userQuery = _context.Users.Find(goodId);
            if (userQuery == null) {
                Console.WriteLine("not found in database");
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetGames", new {Id = goodId}, user);

            }else {
                Console.WriteLine("ahh");
                return Ok(User);

            }

            // // Console.WriteLine(goodId);


        }


        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new {UserName = user.UserName}, user);
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
