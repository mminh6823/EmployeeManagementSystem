

using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class OvertimeType : BaseEntity
    {
        [JsonIgnore]
        List<Overtime>?  Overtimes { get; set; }
    }
}
