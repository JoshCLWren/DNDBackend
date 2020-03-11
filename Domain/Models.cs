using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Domain
{
    public class User

    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<Game> Games {get; set;} 
        // public ICollection<UserGame> UsersGames { get; set; }

    }   
    public class Game
    {
        public long GameId {get; set;}
        public string GameName { get; set; }
        public long UserId { get; set; }
        public User User {get; set;}
        // public ICollection<UserGame> UsersGames { get; set; }
    }
//     public class UserGame
// {
//     [Key]
//     public int UserId { get; set; }
//     public User User { get; set; }

//     public int GameId { get; set; }
//     public Game Game { get; set; }
    
// }
}

