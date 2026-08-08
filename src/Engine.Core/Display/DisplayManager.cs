namespace Engine.Core.Display;

public class DisplayManager
{
    private readonly VirtualResolution _resolution;
    private readonly ViewportCalculator _calculator;

    public DisplayViewport Viewport { get; private set; }

    public DisplayManager(
        VirtualResolution resolution)
    {
        _resolution = resolution;

        _calculator =
            new ViewportCalculator();
    }

    public void Update(
        int x,
        int y,
        int width,
        int height)
    {
        Viewport =
            _calculator.Calculate(
                _resolution,
                x,
                y,
                width,
                height
            );
    }
}