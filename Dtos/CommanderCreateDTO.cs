using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
    public class CommanderCreateDTO
    {
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        [Required]      
        public string Platform { get; set; }
    }
}