using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class VacationType : BaseEntity
    {
        //Mối quan hệ n-1 với Vacation
        public List<Vacation>? Vacations { get; set; }
    }
}
