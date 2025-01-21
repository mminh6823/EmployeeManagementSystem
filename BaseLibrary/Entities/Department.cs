

namespace BaseLibrary.Entities
{
    public class Department : BaseEntity
    {
        //Mối quan hệ n-1 với GeneralDepartment
        public GeneralDepartment? GeneralDepartment { get; set; }
        public int GeneralDepartmentId { get; set; }

        //Mối quan hệ 1-n với Branch

        public List<Branch>? Branches { get; set; }

    }
}
