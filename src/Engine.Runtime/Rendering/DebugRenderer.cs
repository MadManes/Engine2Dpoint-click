using Engine.Runtime.Debug;
using Engine.Runtime.Rendering.DebugRenderers;
using Engine.Runtime.Assets;
using Raylib_cs;
using Engine.Entities;
using Engine.Scene;

namespace Engine.Runtime.Rendering;

public class DebugRenderer : IRenderer
{
    private const int WindowWidth = 1280;
    private const int WindowHeight = 640;
    private const string WindowTitle = "Engine 2D Point & Click - Debug Viewer";

    private readonly PrimitiveRenderer _primitiveRenderer;
    private readonly BackgroundRenderer _backgroundRenderer;
    private readonly DebugSettings _settings;    
    private readonly DebugOverlay _overlay;
    private readonly Camera _camera;
    private readonly WorldDebugRenderer _worldRenderer;
    private readonly AssetManager _assetManager;
    private readonly PlayerDebugRenderer _playerRenderer;
    private readonly SpriteRenderer _spriteRenderer;
    private readonly EntityVisualSorter _entitySorter;
    private readonly NavigationDebugRenderer _navigationRenderer;
    private readonly NavigationInteractionDebugRenderer _navigationInteractionRenderer;

    public DebugRenderer(
        DebugSettings settings,
        Camera camera,
        AssetManager assetManager
        )
    {
        _primitiveRenderer = new PrimitiveRenderer();

        _settings = settings;
        _camera = camera;   
        _assetManager = assetManager;

        _entitySorter = new EntityVisualSorter();

        _overlay = new DebugOverlay(settings);
        _worldRenderer = new WorldDebugRenderer(_primitiveRenderer);
        _backgroundRenderer = new BackgroundRenderer();
        _playerRenderer = new PlayerDebugRenderer(_primitiveRenderer);
        _spriteRenderer = new SpriteRenderer(_assetManager);
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

        Texture2D background = _assetManager.LoadTexture(
            context.Scene.Data.Background.AssetName + ".png"
        );
        _backgroundRenderer.Draw(background);

        Raylib.BeginMode2D(_camera.NativeCamera);

        if (_settings.DrawWorld)
        {
            _worldRenderer.Draw(context.Scene.World);
        }

        foreach (Entity entity in _entitySorter.Sort(
            context.Scene.World.Entities))
        {
            _spriteRenderer.Draw(entity);
        }
        
        Entity? player = GetPlayer(context.Scene);

        if (player != null)
        {
            if (_settings.DrawPlayer)
            {
                _playerRenderer.Draw(player);
            }

            if (_settings.Navigation.DrawPath)
            {
                _navigationRenderer.Draw(player);
            }
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

    private Entity? GetPlayer(GameScene scene)
    {
        return scene.World.Entities.FirstOrDefault(
            entity => entity.Type == Entities.Components.EntityType.Player
        );
    }
    

    public void Shutdown()
    {
        Raylib.CloseWindow();
    }
}