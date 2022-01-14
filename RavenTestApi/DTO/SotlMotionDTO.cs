namespace RavenTestApi.DTO
{
    public class SotlMotionDTO
    {
        public string mEvent { get; set; }
        public Int64 time { get; set; }
        public double xAvg { get; set; }
        public double yAvg { get; set; }
        public double zAvg { get; set; }
        public double xStdev { get; set; }
        public double yStdev { get; set; }
        public double zStdev { get; set; }
        public int xVibAvg { get; set; }
        public int yVibAvg { get; set; }
        public int zVibAvg { get; set; }
        public int xPeakLoc { get; set; }
        public int yPeakLoc { get; set; }
        public int zPeakLoc { get; set; }
        public int xPeakVal { get; set; }
        public int yPeakVal { get; set; }
        public int zPeakVal { get; set; }
        public int vector { get; set; }
    }
}
