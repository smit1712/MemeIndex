using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class MemeCategory
    {
        [Key]
        [Required]
        public string Data { get; set; }

        public MemeCategory(string data)
        {
            Data = data;
        }

        public MemeCategory()
        {

        }
    }
}
