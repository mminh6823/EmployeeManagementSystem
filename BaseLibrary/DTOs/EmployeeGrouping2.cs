
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class EmployeeGrouping2
    {
        [Required]
        public string JobName { get; set; } = string.Empty;
        [Required, Range(1,int.MaxValue , ErrorMessage ="Bạn phải chọn chuyên môn")]
        public int BranchId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Bạn phải chọn thành phố")]
        public  int  CityId { get; set; }
        [Required]
        public string? Other { get; set; }
    }
}
