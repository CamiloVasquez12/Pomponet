﻿using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class Epps
    {
        [Key]
        public int Id_Epp { get; set; }
        public required string Name_Epp { get; set; }
        public bool Deleted { get; set; }

    }
}