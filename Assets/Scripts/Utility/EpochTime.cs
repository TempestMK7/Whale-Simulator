using System;

public class EpochTime {

    public static long CurrentTimeMillis() {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        long currentTime = (long)(System.DateTime.UtcNow - epochStart).TotalMilliseconds;
        return currentTime;
    }
}
