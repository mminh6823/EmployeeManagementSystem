
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Overtime : OtherBaseEntity
    {
        [Required]
        public DateTime StarDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public int NumberOfDay => (EndDate - StarDate).Days;

        //Mối quan hệ n-1 với Vacation Type
        public OvertimeType? OvertimeType { get; set; }
        [Required]
        public int OvertimeTypeId { get; set; }
    }
}
