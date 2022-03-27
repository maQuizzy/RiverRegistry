using RiverRegistry.Models;
using System;
using System.Data;

namespace RiverRegistry.ETL
{
    public static class ShipParser
    {
        public static Ship FromDataRow(DataRow row)
        {
            int waterBallast, year;
            DateTime buildDate;
            int.TryParse(row[6].ToString(), out year);
            int.TryParse(row[39].ToString(), out waterBallast);
            DateTime.TryParse(row[5].ToString(), out buildDate);

            var ship = new Ship
            {
                RegNumber = Convert.ToInt32(row[0]),
                BuildNumber = row[2].ToString(),
                Project = row[3].ToString(),
                Type = row[4].ToString(),
                BuildDate = buildDate,
                Year = year,
                BuildPlace = row[7].ToString(),
                Name = row[1].ToString(),
                Category = row[8].ToString(),
                BodyMaterial = row[30].ToString(),
                SuperStructMaterial = row[31].ToString(),
                WaterBallast = waterBallast,
                SailingConditions = row[40].ToString(),
                ShipCapacity = new ShipCapacity
                {
                    GrossTonnage = Convert.ToDouble(row[15]),
                    NetTonnage = Convert.ToDouble(row[16]),
                    Deadweight = Convert.ToDouble(row[17]),
                    Displacement = Convert.ToDouble(row[18]),
                    Carrying = Convert.ToDouble(row[19]),
                    TransBulk = Convert.ToInt32(row[20]),
                    LongBulk = Convert.ToInt32(row[21]),
                    Passenger = Convert.ToInt32(row[22]),
                    Crew = Convert.ToInt32(row[23]),
                    OrgGroup = Convert.ToInt32(row[24]),
                    BulkTanks = Convert.ToInt32(row[25]),
                    VolumeTanks = Convert.ToDouble(row[26]),
                    Boom1 = Convert.ToDouble(row[27]),
                    Boom2 = Convert.ToDouble(row[28]),
                    Boom3 = Convert.ToDouble(row[29])
                },
                ShipEngines = new ShipEngines
                {
                    MainICEType = row[32].ToString(),
                    MainICEBrand = row[33].ToString(),
                    MainICEPower = Convert.ToInt32(row[34]),
                    MainICECount = Convert.ToInt32(row[35]),
                    RMCount = Convert.ToInt32(row[36]),
                    RMPower = Convert.ToInt32(row[37]),
                    HdPower = Convert.ToInt32(row[38]),
                    FuelCapacity = Convert.ToInt32(row[39])
                },
                ShipDimensions = new ShipDimensions
                {
                    OverLength = Convert.ToDouble(row[9]),
                    ConstrLength = Convert.ToDouble(row[10]),
                    OverWidth = Convert.ToDouble(row[11]),
                    ConstrWidth = Convert.ToDouble(row[12]),
                    Freeboard = Convert.ToDouble(row[13]),
                    BoardHeight = Convert.ToDouble(row[14])
                }
            };           

            return ship;
        }
    }
}
