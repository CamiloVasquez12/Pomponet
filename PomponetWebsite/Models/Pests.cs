﻿using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class Pests
    {
        [Key]
        public int Id_Pest { get; set; }
        public required string Pest { get; set; }
        public bool Deleted { get; set; }
    }
}
