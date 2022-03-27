using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RiverRegistry.Models
{
    public class ShipCapacity
    {
        [Key]
        [ForeignKey("Ship")]
        public int ShipId { get; set; }
        [JsonIgnore]
        public Ship Ship { get; set; }

        public double GrossTonnage { get; set; }
        public double NetTonnage { get; set; }
        public double Deadweight { get; set; }
        public double Displacement { get; set; }
        public double Carrying { get; set; }
        public int TransBulk { get; set; }
        public int LongBulk { get; set; }
        public int Passenger { get; set; }
        public int Crew { get; set; }
        public int OrgGroup { get; set; }
        public int BulkTanks { get; set; }
        public double VolumeTanks { get; set; }
        public double Boom1 { get; set; }
        public double Boom2 { get; set; }
        public double Boom3 { get; set; }
    }
}
