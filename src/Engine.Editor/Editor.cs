using Raylib_cs;
using Engine.Math;
using Engine.Assets;
using Engine.Scene.Definitions;
using Engine.Editor.UI;
using Engine.Editor.Viewport;
using Engine.Editor.Input;
using Engine.Editor.Context;
using Engine.Editor.Tools;
using Engine.Editor.State;
using Engine.Editor.Project;
using Engine.Editor.Loading;
using Engine.Editor.Rendering;
using Engine.Editor.Camera;
using Engine.Editor.Selection;
using Engine.Editor.Documents;
using Engine.Core.Display;
using Engine.Editor.UI.Layout;
using Engine.Editor.Display;
using Engine.Editor.Creation;

namespace Engine.Editor;

public class Editor
{   
    private readonly EditorViewport _viewport;
    private readonly EditorLayout _layout;
    private readonly EditorState _state;
    private readonly EditorProjectSaver _projectSaver;
    private readonly EditorRenderer _renderer;
    private readonly EditorCamera _camera;
    private readonly EditorInputController _input;
    private readonly DisplayManager _displayManager;
    private readonly AssetManager _assetManager;

    private EditorContext _context = null!;

    private readonly ToolManager _toolManager;
    private readonly EditorToolbar _toolbar;
    private readonly EditorContextMenu _contextMenu;

    private readonly EditorEntityPicker _entityPicker;

    private readonly EditorEntityFactory _entityFactory;
    private readonly EditorInspector _inspector;


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

        EditorSceneSaver sceneSaver =
            new EditorSceneSaver(
                "assets/scenes"
            );

        _projectSaver =
            new EditorProjectSaver(
                sceneSaver
            );

        _entityPicker = new EditorEntityPicker(_assetManager);        

        _entityFactory = new EditorEntityFactory();
        _inspector = new EditorInspector();

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

        _toolManager.Register(
            "create",
            new CreateTool(
                _entityFactory
            )            
        );

        _toolbar = new EditorToolbar();

        _contextMenu = new EditorContextMenu();
                        
    }

    private void LoadCurrentSceneAssets()
    {
        EditorAssetLoader assetLoader =
            new(
                _assetManager,
                "assets/sprites"
            );

        _state.SetAssets(
            assetLoader.Load(
                _state.CurrentDocument!
            )
        );

        _inspector.SetAssets(
            _state.Assets!
        );
    }

    private void OpenScene(
        string sceneId)
    {
        _state.OpenScene(
            sceneId
        );

        ResetSceneView();

        LoadCurrentSceneAssets();

        _toolbar.SetCurrentScene(
            sceneId
        );
    }

    private void ResetSceneView()
    {
        _contextMenu.Close();
        _inspector.Close();
    }

    private void InitializeContent()
    {
        EditorProjectLoader loader = new(
            "assets/scenes"
        );        

        SceneProject project =
            loader.Load();

        EditorProject editorProject =
            new(project);

        foreach(SceneData scene in project.Scenes)
        {
            SceneDocument document =
                new(
                    scene,
                    $"{scene.Id}.json"
                );

            editorProject.AddDocument(
                document
            );
        }

        editorProject.OpenDocument(
            "test_room"
        );

        _state.OpenProject(
            editorProject
        );

        _inspector.SetProject(
            editorProject
        );

        _toolbar.SetScenes(
            editorProject.SceneIds
        );

        _toolbar.SetCurrentScene(
            editorProject.CurrentDocument!.Scene.Id
        );

        LoadCurrentSceneAssets();

        _context = new EditorContext(
            _state,
            _input,
            _viewport            
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

        _inspector.UpdateLayout(
            Raylib.GetScreenWidth()
        );

        Raylib.SetExitKey(
            KeyboardKey.Null
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

        _inspector.UpdateLayout(
            width
        );
    }

    private void SaveProject()
    {
        if(_state.Project == null)
            return;
        
        if(!_state.Project.IsDirty)
            return;
        
        _projectSaver.Save(
            _state.Project
        );
    }    

    private void Update()
    {        
        UpdateDisplay();

        _toolbar.SetDocumentState(
            _state.Project?.IsDirty ?? false
        );

        _camera.Update();

        if (_input.RightPressed())
        {
            Vector2 mousePosition =
                _input.MouseWorldPosition();

            EntityData? entity =
                _entityPicker.Pick(
                    _state.CurrentDocument!.Scene.EntityCollection.Entities,
                    mousePosition
                );

            if(entity != null)
            {
                _state.SelectEntity(entity);

                _contextMenu.Open(
                    entity,
                    (int)Raylib.GetMousePosition().X,
                    (int)Raylib.GetMousePosition().Y
                );
            }
            else
            {
                _contextMenu.Close();
            }
        }

        ContextMenuAction contextAction = 
            _contextMenu.Update();

        bool uiConsumedInput = false;
        
        switch(contextAction)
        {
            case ContextMenuAction.Edit:

                if(_contextMenu.Target != null)
                {
                    _inspector.Open(
                        _contextMenu.Target
                    );
                }

                _contextMenu.Close();

                uiConsumedInput = true;

                break;
        }

        InspectorResult inspectorResult =
            _inspector.Update();

        if(inspectorResult == InspectorResult.Changed)
        {            
            _state.MarkCurrentDocumentDirty();            
        }

        if(!_inspector.IsEditing)
        {
            
        }
        
        ToolbarAction action = _toolbar.Update();        

        switch(action)
        {
            case ToolbarAction.Select:
                _toolManager.Activate("select");
                break;

            case ToolbarAction.Move:
                _toolManager.Activate("move");
                break;

            case ToolbarAction.Create:
                _toolManager.Activate("create");
                break;

            case ToolbarAction.Scene:
                if(_toolbar.RequestedSceneId != null)
                {
                    OpenScene(
                        _toolbar.RequestedSceneId
                    );
                }

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

        if(Raylib.IsKeyPressed(
            KeyboardKey.C))
        {
            _toolManager.Activate(
                "create"
            );
        }

        if(!uiConsumedInput)
        {
            _toolManager.Update(
                _context
            );
        }

        _input.EndFrame();

        // Prueba SAVE
        if (
            Raylib.IsKeyDown(KeyboardKey.LeftControl) &&
            Raylib.IsKeyPressed(KeyboardKey.S)
        )
        {
            SaveProject();
        }

        
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
        _inspector.Draw();
        _contextMenu.Draw();

        Raylib.EndDrawing();
    }


    private void Shutdown()
    {
        _assetManager.Dispose();

        Raylib.CloseWindow();
    }
}