using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.InstructorVM
{
    public class InstructorListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string Course { get; set; }
        public string ImgPath { get; set; }
    }
}
