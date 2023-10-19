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

        //Infobox
        public string InfoBoxTitle { get; set; }
        public string InfoBoxDescription { get; set; }
    }
}
