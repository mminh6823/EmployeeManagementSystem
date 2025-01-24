

using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class GeneralDepartment : BaseEntity
    {
        //Mối quan hệ 1-n với Department
        [JsonIgnore]
        List<Department>? departmentList { get; set; }  
    }
}
