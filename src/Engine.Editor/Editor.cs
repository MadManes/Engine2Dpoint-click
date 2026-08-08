using Raylib_cs;
using Engine.Assets;
using Engine.Scene.Definitions;
using Engine.Editor.UI;
using Engine.Editor.Viewport;
using Engine.Editor.Input;
using Engine.Editor.Context;
using Engine.Editor.Tools;
using Engine.Editor.State;
using Engine.Editor.Loading;
using Engine.Editor.Rendering;
using Engine.Editor.Camera;
using Engine.Editor.Selection;
using Engine.Editor.Documents;
using Engine.Core.Display;
using Engine.Editor.UI.Layout;
using Engine.Editor.Display;

namespace Engine.Editor;

public class Editor
{   
    private readonly EditorViewport _viewport;
    private readonly EditorLayout _layout;
    private readonly EditorState _state;    
    private readonly EditorRenderer _renderer;
    private readonly EditorCamera _camera;
    private readonly EditorInputController _input;
    private readonly DisplayManager _displayManager;
    private readonly AssetManager _assetManager;

    private readonly EditorContext _context;
    private readonly ToolManager _toolManager;
    private readonly EditorToolbar _toolbar;

    private readonly EditorEntityPicker _entityPicker;


    public Editor()
    {
        _layout = new EditorLayout();

        _displayManager = 
            new DisplayManager(
                EditorDisplaySettings.Resolution
            );

        _viewport = new EditorViewport(); 

        _state = new EditorState();
        _renderer = new EditorRenderer();
        _camera = new EditorCamera(
            _viewport
        );

        _input = new EditorInputController(
            _camera            
        );
        
        
        _assetManager = new AssetManager("assets");

        _entityPicker = new EditorEntityPicker(_assetManager);

        _context = new EditorContext(
            _state,
            _input,
            _viewport
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

        _toolbar = new EditorToolbar();
                        
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
        Raylib.SetConfigFlags(
            ConfigFlags.ResizableWindow
        );

        Raylib.InitWindow(
            1280,
            640,
            "Engine 2D Editor"
        );

        _layout.Update(
            Raylib.GetScreenWidth(),
            Raylib.GetScreenHeight()
        );

        _toolbar.SetWidth(
            _layout.ToolbarWidth
        );

        _displayManager.Update(
            _layout.SceneX,
            _layout.SceneY,
            _layout.SceneWidth,
            _layout.SceneHeight
        );

        _viewport.Update(
            _displayManager.Viewport
        );
        
    }
    
    private void UpdateDisplay()
    {
        int width =
            Raylib.GetScreenWidth();

        int height =
            Raylib.GetScreenHeight();


        _layout.Update(
            width,
            height
        );


        _toolbar.SetWidth(
            _layout.ToolbarWidth
        );


        _displayManager.Update(
            _layout.SceneX,
            _layout.SceneY,
            _layout.SceneWidth,
            _layout.SceneHeight
        );


        _viewport.Update(
            _displayManager.Viewport
        );
    }

    private void Update()
    {        
        UpdateDisplay();
        _camera.Update();
        
        ToolbarAction action = _toolbar.Update();        

        switch(action)
        {
            case ToolbarAction.Select:
                _toolManager.Activate("select");
                break;

            case ToolbarAction.Move:
                _toolManager.Activate("move");
                break;
        }        
    
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

        _input.EndFrame();
    }


    private void Render()
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(
            Color.Black
        );

        Raylib.BeginScissorMode(
            _viewport.X,
            _viewport.Y,
            _viewport.Width,
            _viewport.Height
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

        Raylib.EndScissorMode();

        _toolbar.Draw();

        Raylib.EndDrawing();
    }


    private void Shutdown()
    {
        _assetManager.Dispose();

        Raylib.CloseWindow();
    }
}