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
        public async Task<ActionResult<User>> Get(int id)
        {
            // create a static class that uses httpheaders get values and then instantiate it in our controller.
            // look up static and void and what they mean.
            // and then call the userinfo endpoint so that the backend has what the frontend has and create post
            // regex out the the social provider and | and then add the sub id value
            
            var responseHeader = Request.Headers["Authorization"].FirstOrDefault();
            Console.WriteLine("=================================");
            Console.WriteLine(responseHeader);
            string[] bearerToken = responseHeader.Split(" ");
            Console.WriteLine("=================================");
            Console.WriteLine(bearerToken[1]);
            var txtJwtIn = bearerToken[1];
            // split works but now I need decode the jwt token...
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(txtJwtIn);
            var tokenS = handler.ReadToken(txtJwtIn) as JwtSecurityToken;
            Console.WriteLine("+++++++++++++++++++++++++++++++++++");
            Console.WriteLine(tokenS);
            // now I need to get the Sub key value pair pulled from tokenS and then split after the | 

            var User = await _context.Users.FindAsync(id);    
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
