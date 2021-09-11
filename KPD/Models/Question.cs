using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPD.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Event { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]    
        public string CorrectAnswer { get; set; }

        public List<Contestant> ContestantsId { get; set; }

        public int WinnerId{ get; set; }

    }
}
