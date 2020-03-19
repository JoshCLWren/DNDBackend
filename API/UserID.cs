using System;
using System.IdentityModel.Tokens.Jwt;

namespace API
{
    public class UserIDClass
    {
        protected string responseHeader = "";
        protected long id = 0;
        public long idMethod(string responseHeader, long id)
        {
            
            string[] bearerToken = responseHeader.Split(" ");
            var txtJwtIn = bearerToken[1];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(txtJwtIn);
            var decodedToken = handler.ReadToken(txtJwtIn) as JwtSecurityToken;
            var sub = decodedToken.Subject;
            string[] subSplit = sub.Split("|");
            long goodId = Int64.Parse(subSplit[1]);
            if (goodId == id)
                {
                    Console.WriteLine("goodId "+ goodId + "  = id " + id);
                }
                else {
                
                Console.WriteLine("goodId = " + goodId + " but the id you searched was " + id);
                    // post method here... I can use long goodId for id, nickname for username, but where do I get email? do I need email?
                    // I'll post a dummy value for email and maybe decide to not use it 
                    // private static readonly HttpClient client = new HttpClient();
                    } 
            return goodId;
        }
        
        
}}