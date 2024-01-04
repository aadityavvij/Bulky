﻿using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Data
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public string Summary { get; set; }
        [MaxLength(3)]
        public List<Actor> Actors { get; set; }
    }
}
