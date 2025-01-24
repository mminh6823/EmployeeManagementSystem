using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class VacationType : BaseEntity
    {
        //Mối quan hệ n-1 với Vacation
        [JsonIgnore]
        public List<Vacation>? Vacations { get; set; }
    }
}
