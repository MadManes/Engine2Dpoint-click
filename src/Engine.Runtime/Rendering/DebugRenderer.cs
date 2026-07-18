using Engine.Math;
using Engine.Scene.World;
using Raylib_cs;

namespace Engine.Runtime.Rendering;

public class DebugRenderer : IRenderer
{
    private const int WindowWidth = 1280;
    private const int WindowHeight = 640;
    private const string WindowTitle = "Engine 2D Point & Click - Debug Viewer";

    private readonly PrimitiveRenderer _primitiveRenderer;

    public DebugRenderer()
    {
        _primitiveRenderer = new PrimitiveRenderer();
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

        DrawWorld(context.World);
        
        DrawMouse(context);

        DrawCorrectedPoint(context);

        DrawNavigationCorrection(context);

        Raylib.DrawFPS(10, 10);

        Raylib.EndDrawing();
    }

    private void DrawWorld(World world)
    {
        WalkableArea area = (WalkableArea)world.WalkableArea;

        DrawPolygon(area.Polygon);        
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