using Engine.Math;
using Engine.Runtime.Rendering;
using Raylib_cs;

namespace Engine.Runtime.Rendering.DebugRenderers;

public class NavigationInteractionDebugRenderer
{
    private readonly PrimitiveRenderer _primitiveRenderer;


    public NavigationInteractionDebugRenderer(
        PrimitiveRenderer primitiveRenderer)
    {
        _primitiveRenderer = primitiveRenderer;
    }


    public void Draw(
        Vector2? mousePosition,
        Vector2? correctedPosition)
    {
        DrawMouse(mousePosition);

        DrawCorrectedPoint(correctedPosition);

        DrawCorrectionLine(
            mousePosition,
            correctedPosition
        );
    }


    private void DrawMouse(Vector2? mousePosition)
    {
        if (!mousePosition.HasValue)
            return;


        _primitiveRenderer.DrawCircle(
            mousePosition.Value,
            8,
            Color.Yellow
        );
    }


    private void DrawCorrectedPoint(
        Vector2? correctedPosition)
    {
        if (!correctedPosition.HasValue)
            return;


        _primitiveRenderer.DrawCircle(
            correctedPosition.Value,
            8,
            Color.Red
        );
    }


    private void DrawCorrectionLine(
        Vector2? mousePosition,
        Vector2? correctedPosition)
    {
        if (!mousePosition.HasValue ||
            !correctedPosition.HasValue)
        {
            return;
        }


        _primitiveRenderer.DrawLine(
            mousePosition.Value,
            correctedPosition.Value,
            Color.Yellow
        );
    }
}