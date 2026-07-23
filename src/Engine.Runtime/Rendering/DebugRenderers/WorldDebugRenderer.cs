using Engine.Math;
using Engine.Scene.World;
using Raylib_cs;
using Engine.Runtime.Rendering;

namespace Engine.Runtime.Rendering.DebugRenderers;

public class WorldDebugRenderer
{
    private readonly PrimitiveRenderer _primitiveRenderer;

    public WorldDebugRenderer(PrimitiveRenderer primitiveRenderer)
    {
        _primitiveRenderer = primitiveRenderer;
    }


    public void Draw(SceneWorld world)
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
}