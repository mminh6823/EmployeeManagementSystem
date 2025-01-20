using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class BaseDepartmentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Tên không được để trống!"), MaxLength(50), MinLength(3)]
        public string?  Name{ get; set; }
    }
}
