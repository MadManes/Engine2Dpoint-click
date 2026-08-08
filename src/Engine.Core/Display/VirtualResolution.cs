namespace Engine.Core.Display;

public readonly struct VirtualResolution
{
    public int Width { get; }

    public int Height { get; }


    public VirtualResolution(
        int width,
        int height)
    {
        if(width <= 0)
            throw new ArgumentException(
                "Width must be greater than zero"
            );


        if(height <= 0)
            throw new ArgumentException(
                "Height must be greater than zero"
            );


        Width = width;
        Height = height;
    }
}