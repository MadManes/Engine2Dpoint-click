using Engine.Core;
using Engine.Entities;
using Engine.Runtime.Debug;
using Raylib_cs;

namespace Engine.Runtime.Rendering.DebugRenderers;

public class NavigationDebugRenderer
{
    private readonly PrimitiveRenderer _primitiveRenderer;
    private readonly NavigationDebugSettings _settings;


    public NavigationDebugRenderer(
        PrimitiveRenderer primitiveRenderer,
        NavigationDebugSettings settings)
    {
        _primitiveRenderer = primitiveRenderer;
        _settings = settings;
    }


    public void Draw(Entity player)
    {
        NavigationPath? path = player.Movement.Path;

        if (path == null || path.IsEmpty)
            return;


        if (_settings.DrawConnections)
        {
            DrawPathConnections(path);
        }


        if (_settings.DrawNodes)
        {
            DrawPathNodes(path);
        }


        if (_settings.DrawCurrentTarget)
        {
            DrawCurrentTarget(player, path);
        }
    }


    private void DrawPathConnections(
        NavigationPath path)
    {
        IReadOnlyList<Engine.Math.Vector2> points = path.Points;

        for (int i = 0; i < points.Count - 1; i++)
        {
            _primitiveRenderer.DrawLine(
                points[i],
                points[i + 1],
                Color.Purple
            );
        }
    }


    private void DrawPathNodes(
        NavigationPath path)
    {
        IReadOnlyList<Engine.Math.Vector2> points = path.Points;

        for (int i = 0; i < points.Count; i++)
        {
            Color color =
                i == path.CurrentIndex
                ? Color.Yellow
                : Color.Purple;


            _primitiveRenderer.DrawCircle(
                points[i],
                8,
                color
            );
        }
    }


    private void DrawCurrentTarget(
        Entity player,
        NavigationPath path)
    {
        if (!path.HasCurrentPoint)
            return;


        _primitiveRenderer.DrawLine(
            player.Transform.Position,
            path.CurrentPoint,
            Color.Yellow
        );
    }
}