namespace RavenTestApi.DTO
{
    public class QiLogsDTO
    {
        public DateTime time { get; set; }
        public string message { get; set; }
        public string program { get; set; }
        public string tag { get; set; }
        public string exceptionText { get; set; }
        public int exCode { get; set; }
        public string level { get; set; }
        public string user { get; set; }
    }
}
