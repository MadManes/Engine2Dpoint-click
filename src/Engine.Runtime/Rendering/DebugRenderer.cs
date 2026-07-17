using Raylib_cs;

namespace Engine.Runtime.Rendering;

public class DebugRenderer
{
    private const int WindowWidth = 1280;
    private const int WindowHeight = 720;
    private const string WindowTitle = "Engine 2D Point & Click - Debug Viewer";

    public void Initialize()
    {
        Raylib.InitWindow(WindowWidth, WindowHeight, WindowTitle);
        Raylib.SetTargetFPS(60);
    }

    public void Render()
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.DarkBlue);

        Raylib.DrawFPS(10, 10);

        Raylib.EndDrawing();
    }

    public void Shutdown()
    {
        Raylib.CloseWindow();
    }
}