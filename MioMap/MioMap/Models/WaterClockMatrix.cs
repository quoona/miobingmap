namespace MioMap.Models
{
    public class WaterClockMatrix : WaterClock
    {
        public WaterClockMatrix(WaterClock data, int index)
        {
            this.Id = data.Id;
            this.Lat = data.Lat;
            this.Long = data.Long;
            this.Title = data.Title;
            this.Description = data.Description;
            this.Address = data.Address;
            this.WaterClockStatus = data.WaterClockStatus;
            this.InWaterClock = data.InWaterClock;
            this.OutWaterClock = data.OutWaterClock;
            this.InfoBoxTitle = data.InfoBoxTitle;
            this.InfoBoxDescription = data.InfoBoxDescription;
            this.Index = index;
        }

        public int Index { get; set; }
    }
}
