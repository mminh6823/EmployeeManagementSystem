using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Vacation : OtherBaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int NumberOfDays { get; set; }

        public DateTime EndDate  => StartDate.AddDays(NumberOfDays);

        //Mối quan hệ n-1 với VacationType
        public VacationType? VacationType { get; set; }
        [Required]
        public int VacationTypeId { get; set; }
    }
}
