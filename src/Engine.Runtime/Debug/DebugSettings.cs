namespace Engine.Runtime.Debug;

public class DebugSettings
{
    public bool DrawWorld { get; set; } = true;

    public bool DrawPlayer { get; set; } = true;

    public bool DrawMouse { get; set; } = true;

    public bool DrawCorrectedPoint { get; set; } = true;

    public bool DrawNavigationCorrection { get; set; } = true;

    public bool DrawFPS { get; set; } = true;

    public NavigationDebugSettings Navigation { get; } = new();

    public void ToggleWorld()
    {
        DrawWorld = !DrawWorld;
    }

    public void TogglePlayer()
    {
        DrawPlayer = !DrawPlayer;
    }

    public void ToggleMouse()
    {
        DrawMouse = !DrawMouse;
    }

    public void ToggleCorrectedPoint()
    {
        DrawCorrectedPoint = !DrawCorrectedPoint;
    }

    public void ToggleNavigationCorrection()
    {
        DrawNavigationCorrection = !DrawNavigationCorrection;
    }

    public void ToggleFPS()
    {
        DrawFPS = !DrawFPS;
    }
}