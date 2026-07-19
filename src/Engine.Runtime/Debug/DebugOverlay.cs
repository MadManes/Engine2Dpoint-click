using Raylib_cs;

namespace Engine.Runtime.Debug;

public class DebugOverlay
{
    private readonly DebugSettings _settings;

    public DebugOverlay(DebugSettings settings)
    {
        _settings = settings;
    }


    public void Draw()
    {
        int x = 10;
        int y = 10;
        int spacing = 20;

        Raylib.DrawText(
            "DEBUG MODE",
            x,
            y,
            20,
            Color.White
        );

        y += spacing * 2;


        DrawLine(
            "F1 World",
            _settings.DrawWorld,
            x,
            y
        );

        y += spacing;

        DrawLine(
            "F2 Player",
            _settings.DrawPlayer,
            x,
            y
        );

        y += spacing;

        DrawLine(
            "F3 Mouse",
            _settings.DrawMouse,
            x,
            y
        );

        y += spacing;

        DrawLine(
            "F4 Corrected Point",
            _settings.DrawCorrectedPoint,
            x,
            y
        );

        y += spacing;

        DrawLine(
            "F5 Navigation",
            _settings.DrawNavigationCorrection,
            x,
            y
        );

        y += spacing;

        DrawLine(
            "F6 FPS",
            _settings.DrawFPS,
            x,
            y
        );
    }


    private void DrawLine(
        string text,
        bool enabled,
        int x,
        int y)
    {
        string state =
            enabled ? "ON" : "OFF";

        Raylib.DrawText(
            $"{text}: {state}",
            x,
            y,
            18,
            Color.White
        );
    }
}