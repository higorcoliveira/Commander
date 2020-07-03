using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class Command
    {
        // convenção sobre configuração, é Id e tem todas as características de um ID
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }
        
        [Required]
        public string Platform { get; set; }
    }
}