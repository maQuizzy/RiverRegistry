using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiverRegistry.Models
{
    public class Ship
    {
        public int ShipId { get; set; }

        public int RegNumber { get; set; }
        public string BuildNumber { get; set; }
        public string Project { get; set; }
        public string Type { get; set; }

        [Column(TypeName = "Date")]
        public DateTime BuildDate { get; set; }
        public int Year { get; set; }
        public string BuildPlace { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string BodyMaterial { get; set; }
        public string SuperStructMaterial { get; set; }
        public int WaterBallast { get; set; }
        public string SailingConditions { get; set; }

        [Required]
        public ShipCapacity ShipCapacity { get; set; }
        [Required]
        public ShipEngines ShipEngines { get; set; }
        [Required]
        public ShipDimensions ShipDimensions { get; set; }
    }
}
