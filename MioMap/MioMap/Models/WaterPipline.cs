using System.ComponentModel.DataAnnotations;

namespace MioMap.Models
{
    public class WaterPipline
    {
        [Key]
        public int Id { get; set; }
        public double Lat1 { get; set; }
        public double Long1 { get; set; }
        public double Lat2 { get; set; }
        public double Long2 { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //Infobox
        public string InfoBoxTitle { get; set; }
        public string InfoBoxDescription { get; set; }
        // search tags
        public string Tags { get; set; }
    }
}
