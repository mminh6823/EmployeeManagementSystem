

namespace BaseLibrary.Entities
{
    public class GeneralDepartment : BaseEntity
    {
        //Mối quan hệ 1-n với Department
        List<Department>? departmentList { get; set; }  
    }
}
