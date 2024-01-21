using System.ComponentModel.DataAnnotations;

namespace Edukate.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        [Required,MaxLength(32),MinLength(3)]
        public string Name { get; set; }
        [Required, MaxLength(32), MinLength(5)]
        public string Course { get; set; }
        public string ImgPath { get; set; }
    }
}
