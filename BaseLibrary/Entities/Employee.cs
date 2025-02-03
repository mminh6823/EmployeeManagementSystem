using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Employee  : BaseEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? CivilId { get; set; }
        [Required]
        public string? FileNumber { get; set; }
        [Required]
        public string? Jobname { get; set; }
        [Required]
        public string? Address{ get; set; }
        [Required,DataType(DataType.PhoneNumber)]
        public string?  TelephoneNumber{ get; set; }
        [Required]
        public string? Photo { get; set; }
        [Required]
        public string? Other { get; set; }



        //Mối quan hệ n-1 với Branch và City
        public Branch? Branch { get; set; }
        public City? City { get; set; }
        public int BranchId { get; set; }
        public int CityId { get; set; }
    }
}
