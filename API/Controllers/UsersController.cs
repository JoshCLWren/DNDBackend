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
        public async Task<ActionResult<User>> Get(long id)
        {
            // create a static class that uses httpheaders get values and then instantiate it in our controller.
            // look up static and void and what they mean.
            // and then call the userinfo endpoint so that the backend has what the frontend has and create post
            // regex out the the social provider and | and then add the sub id value
            
            // abstract this into a class. leave the header obviosly as it will be fed into the class or method?
            var responseHeader = Request.Headers["Authorization"].FirstOrDefault();
            Console.WriteLine("=================================");
            Console.WriteLine(responseHeader);
            string[] bearerToken = responseHeader.Split(" ");
            Console.WriteLine("=================================");
            Console.WriteLine(bearerToken[1]);
            var txtJwtIn = bearerToken[1];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(txtJwtIn);
            var decodedToken = handler.ReadToken(txtJwtIn) as JwtSecurityToken;
            Console.WriteLine("+++++++++++++++++++++++++++++++++++");
            Console.WriteLine(decodedToken.Subject);
            var sub = decodedToken.Subject;
            Console.WriteLine("SUB+++++++++++++++++++++++++++++++++++");
            Console.WriteLine(sub);
            string[] subSplit = sub.Split("|");
            Console.WriteLine("postpipe||||||||||||||||||||||||||||||||||");
            Console.WriteLine(subSplit[1]);
            long goodId = Int64.Parse(subSplit[1]);
            Console.WriteLine("interger time -------------------------");
            Console.WriteLine(goodId);
            // var values = new Dictionary<string, string>
            //             {
            //             { "thing1", "hello" },
            //             { "thing2", "world" }
            //             };

            // var content = new FormUrlEncodedContent(values);

            // var response = await client.PostAsync("http://www.example.com/recepticle.aspx", content);

            // var responseString = await response.Content.ReadAsStringAsync();
            // //  does goodId = id?
            if (goodId == id)
                {
                    Console.WriteLine("goodId = id");
                }
                else {
                    Console.WriteLine("goodId = " + goodId + " but the id you searched was " + id);
                    // post method here... I can use long goodId for id, nickname for username, but where do I get email? do I need email?
                    // I'll post a dummy value for email and maybe decide to not use it 
                    // private static readonly HttpClient client = new HttpClient();
                    } 

            var User = await _context.Users.FindAsync(id);    
            //https://docs.microsoft.com/en-us/ef/ef6/querying/ 
            // don't call your api inside your api
            return Ok(User);

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
