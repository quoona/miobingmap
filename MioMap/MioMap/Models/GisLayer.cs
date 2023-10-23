using System.ComponentModel.DataAnnotations;

namespace MioMap.Models
{
    public class GisLayer
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public List<WaterPipline> WaterPiplines { get; set; }
    }
}
