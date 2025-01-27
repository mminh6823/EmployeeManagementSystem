

using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class City : BaseEntity
    {
        //Mối quan hệ n-1 với Country
        public Country? Country { get; set; }
        public int CountryId { get; set; }
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }

    }
}
