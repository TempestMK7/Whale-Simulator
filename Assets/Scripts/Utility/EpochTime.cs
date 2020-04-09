using System;

public class EpochTime {

    public static long CurrentTimeMillis() {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        long currentTime = (long)(DateTime.UtcNow - epochStart).TotalMilliseconds;
        return currentTime;
    }

    public static string GetCurrentDate() {
        return new DateTime().ToString("yyyy-MM-dd");
    }
}
