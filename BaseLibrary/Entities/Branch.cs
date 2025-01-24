

using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Branch : BaseEntity
    {
        //Mối quan hệ n-1 với Department
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        //Mối quan hệ 1-n với Employee
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }
    }
}
