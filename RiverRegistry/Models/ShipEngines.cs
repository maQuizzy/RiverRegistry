using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RiverRegistry.Models
{
    public class ShipEngines
    {
        [Key]
        [ForeignKey("Ship")]
        public int ShipId { get; set; }
        [JsonIgnore]
        public Ship Ship { get; set; }

        public string MainICEType { get; set; }
        public string MainICEBrand { get; set; }
        public int MainICEPower { get; set; }
        public int MainICECount { get; set; }
        public int RMCount { get; set; }
        public int RMPower { get; set; }
        public int HdPower { get; set; }
        public int FuelCapacity { get; set; }
    }
}
