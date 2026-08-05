using Engine.Assets;
using Engine.Scene.Definitions;
using Engine.Editor.Input;
using Engine.Editor.Context;
using Engine.Editor.Tools;
using Engine.Editor.State;
using Engine.Editor.Loading;
using Engine.Editor.Rendering;
using Engine.Editor.Camera;
using Engine.Editor.Selection;
using Engine.Editor.Documents;
using Raylib_cs;

namespace Engine.Editor;

public class Editor
{    
    private readonly EditorState _state;    
    private readonly EditorRenderer _renderer;
    private readonly EditorCamera _camera;
    private readonly EditorInputController _input;
    private readonly AssetManager _assetManager;

    private readonly EditorContext _context;
    private readonly ToolManager _toolManager;

    private readonly EditorEntityPicker _entityPicker;


    public Editor()
    {           
        _state = new EditorState();
        _renderer = new EditorRenderer();
        _camera = new EditorCamera();

        _input = new EditorInputController(
            _camera
        );
        
        
        _assetManager = new AssetManager("assets");

        _entityPicker = new EditorEntityPicker(_assetManager);

        _context = new EditorContext(
            _state,
            _input
        );

        _toolManager = new ToolManager();

        _toolManager.Register(
            "select",
            new SelectTool(
                _entityPicker
            )
        );

        _toolManager.Register(
            "move",
            new MoveTool(
                _entityPicker
            )
        );
                        
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
    
    
        if(Raylib.IsKeyPressed(
            KeyboardKey.S))
        {
            _toolManager.Activate(
                "select"
            );
        }


        if(Raylib.IsKeyPressed(
            KeyboardKey.M))
        {
            _toolManager.Activate(
                "move"
            );
        }


        _toolManager.Update(
            _context
        );
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
            _state.Assets!,
            _state.SelectedEntity
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