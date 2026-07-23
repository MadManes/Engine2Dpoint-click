using Engine.Runtime.Debug;
using Engine.Runtime.Rendering.DebugRenderers;
using Engine.Scene.World;
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
    private readonly Camera _camera;

    private readonly WorldDebugRenderer _worldRenderer;
    private readonly PlayerDebugRenderer _playerRenderer;
    private readonly NavigationDebugRenderer _navigationRenderer;
    private readonly NavigationInteractionDebugRenderer _navigationInteractionRenderer;

    public DebugRenderer(
        DebugSettings settings,
        Camera camera)
    {
        _primitiveRenderer = new PrimitiveRenderer();
        _settings = settings;
        _camera = camera;        
        _overlay = new DebugOverlay(settings);
        _worldRenderer = new WorldDebugRenderer(_primitiveRenderer);
        _playerRenderer = new PlayerDebugRenderer(_primitiveRenderer);
        _navigationRenderer = new NavigationDebugRenderer(
            _primitiveRenderer,
            settings.Navigation
        );
        _navigationInteractionRenderer = new NavigationInteractionDebugRenderer(
            _primitiveRenderer
        );
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

        Raylib.BeginMode2D(_camera.NativeCamera);

        if (_settings.DrawWorld)
        {
            _worldRenderer.Draw(context.Scene.World);
        }

        if (_settings.DrawPlayer)
        {
            _playerRenderer.Draw(context.Player);
        }

        if (_settings.Navigation.DrawPath)
        {
            _navigationRenderer.Draw(context.Player);
        }

        if (_settings.DrawMouse ||
            _settings.DrawCorrectedPoint ||
            _settings.DrawNavigationCorrection)
        {
            _navigationInteractionRenderer.Draw(
                context.MousePosition,
                context.CorrectedPosition
            );
        }

        Raylib.EndMode2D();

        if (_settings.DrawFPS)
        {
            Raylib.DrawFPS(1120, 10);
        }

        _overlay.Draw();

        Raylib.EndDrawing();
    }
    

    public void Shutdown()
    {
        Raylib.CloseWindow();
    }
}