using System;
using System.ComponentModel.DataAnnotations;

namespace async_inn.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
