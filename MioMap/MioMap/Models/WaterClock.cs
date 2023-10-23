using System.ComponentModel.DataAnnotations;

namespace MioMap.Models
{
    public class WaterClock
    {
        [Key]
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int WaterClockStatus { get; set; }
        public string InWaterClock { get; set; } // "1,2"
        public string OutWaterClock { get; set; } // "3,4"

        //Infobox
        public string InfoBoxTitle { get; set; }
        public string InfoBoxDescription { get; set; }
    }
}
