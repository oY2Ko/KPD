using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPD.Models
{
    public class Contestant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public string Answer { get; set; }

        public int QuestionId { get; set; }


    }
}
