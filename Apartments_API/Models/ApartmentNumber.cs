﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apartment_API.Models
{
    public class ApartmentNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] // Apartment number must be provided by the User
        public int ApartmentNo { get; set; }
        public string SpecialProperties{ get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
