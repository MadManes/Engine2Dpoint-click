using Engine.Math;
using Engine.Scene.World;
using Engine.Runtime.Debug;
using Raylib_cs;

namespace Engine.Runtime.Rendering;

public class DebugRenderer : IRenderer
{
    private const int WindowWidth = 1280;
    private const int WindowHeight = 640;
    private const string WindowTitle = "Engine 2D Point & Click - Debug Viewer";

    private readonly PrimitiveRenderer _primitiveRenderer;
    private readonly DebugSettings _settings;
    private readonly DebugOverlay _overlay;

    public DebugRenderer(DebugSettings settings)
    {
        _primitiveRenderer = new PrimitiveRenderer();
        _settings = settings;
        _overlay = new DebugOverlay(settings);
    }

    public void Initialize()
    {
        Raylib.InitWindow(WindowWidth, WindowHeight, WindowTitle);
        Raylib.SetTargetFPS(60);
    }

    public void Render(RenderContext context)    
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.DarkBlue);

        if (_settings.DrawWorld)
        {
            DrawWorld(context.World);
        }

        if (_settings.DrawPlayer)
        {
            DrawPlayer(context);
        }

        if (_settings.DrawMouse)
        {
            DrawMouse(context);
        }

        if (_settings.DrawCorrectedPoint)
        {
            DrawCorrectedPoint(context);
        }

        if (_settings.DrawNavigationCorrection)
        {
            DrawNavigationCorrection(context);
        }

        if (_settings.DrawFPS)
        {
            Raylib.DrawFPS(1120, 10);
        }

        _overlay.Draw();

        Raylib.EndDrawing();
    }

    private void DrawWorld(World world)
    {
        WalkableArea area = (WalkableArea)world.WalkableArea;

        DrawPolygon(area.Polygon);        
    }

    private void DrawPlayer(RenderContext context)
    {
        _primitiveRenderer.DrawCircle(
            context.Player.Transform.Position,
            10,
            Color.SkyBlue
        );
    }

    private void DrawPolygon(Polygon polygon)
    {
        IReadOnlyList<Vector2> vertices = polygon.Vertices;

        for (int i = 0; i < vertices.Count; i++)
        {
            Vector2 start = vertices[i];
            Vector2 end = vertices[(i + 1) % vertices.Count];

            _primitiveRenderer.DrawLine(
                start,
                end,
                Color.Green
            );
        }
        
        DrawVertices(polygon);
    }

    private void DrawVertices(Polygon polygon)
    {
        foreach (Vector2 vertex in polygon.Vertices)
        {
            _primitiveRenderer.DrawCircle(
                vertex,
                5,
                Color.Red
            );
        }
    }

    private void DrawMouse(RenderContext context)
    {
        if (context.MousePosition.HasValue)
        {
            _primitiveRenderer.DrawCircle(
                context.MousePosition.Value,
                8,
                Color.Yellow
            );
        }
    }

    private void DrawCorrectedPoint(RenderContext context)
    {
        if (context.CorrectedPosition.HasValue)
        {
            _primitiveRenderer.DrawCircle(
                context.CorrectedPosition.Value,
                8,
                Color.Red
            );
        }
    }

    private void DrawNavigationCorrection(RenderContext context)
    {
        if (context.MousePosition.HasValue &&
            context.CorrectedPosition.HasValue)
        {
            _primitiveRenderer.DrawLine(
                context.MousePosition.Value,
                context.CorrectedPosition.Value,
                Color.Yellow
            );
        }
    }

    public void Shutdown()
    {
        Raylib.CloseWindow();
    }
}