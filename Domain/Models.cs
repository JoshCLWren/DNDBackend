using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

namespace Domain
{
    public class User

    {
        private readonly ILazyLoader _lazyLoader;
        public User()
        {
            
        }
        public User(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        private List<Game> _games;
        public long Id { get; set; }
        public string UserName { get; set; }
        public List<Game> Games
        {
            get => _lazyLoader.Load(this, ref _games);
            set => _games = value;
        }
 
    }   
    public class Game
    {
        //naming convention for gameid uuid? universal unique id
        public Guid GameId {get; set;}
        public string GameName { get; set; }
        public long UserId { get; set; }
        public User User {get; set;}
    }
}