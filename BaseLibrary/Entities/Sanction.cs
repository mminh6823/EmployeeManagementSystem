

using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Sanction : OtherBaseEntity
    {
        [Required]
      public DateTime Date { get; set; }
        [Required]
        public string Punishment { get; set; } =string.Empty;
        [Required]
        public DateTime? PunishmentDate { get; set; }

        //Mối quan hệ n-1 với VacationType
 
        public SanctionType? SanctionType { get; set; }
        public int SanctionTypeId { get; set; }
    }
}
