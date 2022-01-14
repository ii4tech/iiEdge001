using RavenTestApi.DTO;

namespace RavenTestApi.Models
{
    public class sotl6mdpack
    {
        public int deviceId { get; set; }
        public int entityId { get; set; }
        public string level { get; set; }
        public string obj { get; set; }
        public SotlMotionDTO? body { get; set; }   
        public SotlAccelDTO? accel { get; set; }
        public RawAccelDTO? rawAccel { get; set; }
    }
}
