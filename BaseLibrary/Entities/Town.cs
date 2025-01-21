

namespace BaseLibrary.Entities
{
    public class Town : BaseEntity
    {
        //Mối quan hệ 1-n với Employee
        public List<Employee> Employees { get; set; }
        //Mối quan hệ n-1 với City
        public City? City { get; set; }
        public int CityId { get; set; }
    }
}
