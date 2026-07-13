namespace Engine.Core;

public static class Time
{
    private static DateTime _lastFrame = DateTime.Now;

    public static float DeltaTime()
    {
        DateTime now = DateTime.Now;
        float delta = (float)(now - _lastFrame).TotalSeconds;
        _lastFrame = now;
        return delta;
    }
}