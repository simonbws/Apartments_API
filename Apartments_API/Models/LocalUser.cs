﻿using System.Reflection.Metadata.Ecma335;

namespace Apartment_API.Models
{
    public class LocalUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
