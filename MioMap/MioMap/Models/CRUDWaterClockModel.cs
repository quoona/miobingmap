using System.ComponentModel.DataAnnotations;

namespace MioMap.Models
{
    public class CRUDWaterClockModel
    {
        [Key]
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int WaterClockStatus { get; set; }
        public List<int> InWaterClock { get; set; } // "1,2"
        public List<int> OutWaterClock { get; set; } // "3,4"
    }
}
