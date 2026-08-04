using Engine.Assets;
using Engine.Editor.State;
using Engine.Editor.Loading;
using Engine.Editor.Rendering;
using Engine.Editor.Camera;
using Engine.Scene.Definitions;
using Engine.Editor.Documents;
using Raylib_cs;

namespace Engine.Editor;

public class Editor
{
    private readonly EditorState _state;    
    private readonly EditorRenderer _renderer;
    private readonly EditorCamera _camera;
    private readonly AssetManager _assetManager;


    public Editor()
    {
        _state = new EditorState();
        _renderer = new EditorRenderer();
        _camera = new EditorCamera();
        
        _assetManager = new AssetManager("assets");
                
    }

    private void InitializeContent()
    {
        EditorProjectLoader loader = new(
            "assets/scenes"
        );        

        SceneProject project = loader.Load();

        SceneData scene = project.GetScene("test_room");

        SceneDocument document = new(
            scene,
            "test_room.json"
        );

        _state.OpenDocument(document);

        EditorAssetLoader assetLoader =
            new(_assetManager);

        _state.Assets =
            assetLoader.Load(
                _state.CurrentDocument!
            );
    }

    public void Run()
    {
        Initialize();

        InitializeContent();

        while (!Raylib.WindowShouldClose())
        {
            Update();
            Render();
        }


        Shutdown();
    }


    private void Initialize()
    {
        Raylib.InitWindow(
            1280,
            640,
            "Engine 2D Editor"
        );
    }


    private void Update()
    {
        _camera.Update();
    }


    private void Render()
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(
            Color.Black
        );

        Raylib.BeginMode2D(
            _camera.NativeCamera
        );

        _renderer.Render(
            _state.CurrentDocument!,
            _state.Assets!
        );

        Raylib.EndMode2D();

        Raylib.EndDrawing();
    }


    private void Shutdown()
    {
        _assetManager.Dispose();

        Raylib.CloseWindow();
    }
}