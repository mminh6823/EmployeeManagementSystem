

namespace BaseLibrary.Entities
{
    public class Country : BaseEntity
    {
        //Mối quan hệ 1-n với City
        public List<City>? Cities { get; set; } 
    }
}
