using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RiverRegistry.Models
{
    public class ShipDimensions
    {
        [Key]
        [ForeignKey("Ship")]
        public int ShipId { get; set; }
        [JsonIgnore]
        public Ship Ship { get; set; }

        public double OverLength { get; set; }
        public double ConstrLength { get; set; }
        public double OverWidth { get; set; }
        public double ConstrWidth { get; set; }
        public double Freeboard { get; set; }
        public double BoardHeight { get; set; }
    }
}
