namespace Engine.Core.Display;

public class ViewportCalculator
{
    public DisplayViewport Calculate(
        VirtualResolution virtualResolution,
        int areaX,
        int areaY,
        int areaWidth,
        int areaHeight)
    {
        float scaleX =
            areaWidth /
            (float)virtualResolution.Width;

        float scaleY =
            areaHeight /
            (float)virtualResolution.Height;

        float scale =
            System.Math.Min(
                scaleX,
                scaleY
            );

        int viewportWidth =
            (int)(
                virtualResolution.Width *
                scale
            );

        int viewportHeight =
            (int)(
                virtualResolution.Height *
                scale
            );

        int x =
            areaX +
            (areaWidth - viewportWidth) / 2;

        int y =
            areaY +
            (areaHeight - viewportHeight) / 2;

        return new DisplayViewport(
            x,
            y,
            viewportWidth,
            viewportHeight,
            scale
        );
    }
}