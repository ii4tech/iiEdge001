namespace RavenTestApi.Services
{
    public static class Util
    {
        public static string FormatDateTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public static string FormatDateTimeMillis(DateTime dt)
        {
            return dt.ToString("yyyy-MM-ddTHH:mm:ss.sss");
        }

        public static Int64 getEpoch(DateTime dt)
        {
            DateTimeOffset dtoff = dt;
            return dtoff.ToUnixTimeMilliseconds();
        }
    }
}
