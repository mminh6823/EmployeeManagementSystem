

namespace BaseLibrary.Entities
{
    public class City : BaseEntity
    {
        //Mối quan hệ n-1 với Country
        public Country? Country { get; set; }
        public int CountryId { get; set; }


        //Mối quan hệ 1-n với Town
        public List<Town>? Towns { get; set; }
    }
}
