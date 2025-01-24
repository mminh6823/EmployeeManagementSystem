

using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Country : BaseEntity
    {
        //Mối quan hệ 1-n với City
        [JsonIgnore]
        public List<City>? Cities { get; set; } 
    }
}
