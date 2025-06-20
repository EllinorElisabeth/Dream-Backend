using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.Models;

    public class Thought 
    {
        [Key]
        public int Id { get; set; } 
        
        public string? ThoughtText { get; set; }
    }

