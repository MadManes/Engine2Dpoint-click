namespace Engine.Core.Display;

public readonly struct DisplayViewport
{
    public int X { get; }

    public int Y { get; }

    public int Width { get; }

    public int Height { get; }

    public float Scale { get; }


    public DisplayViewport(
        int x,
        int y,
        int width,
        int height,
        float scale)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        Scale = scale;
    }
}