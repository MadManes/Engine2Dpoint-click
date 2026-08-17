using Raylib_cs;
using Engine.Math;
using Engine.Assets;
using Engine.Core.Display;

using Engine.Scene.Definitions;
using Engine.Scene.Serialization;
using Engine.Scene.Loading.Project;

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
    
    
    private readonly SceneCreationPopup _sceneCreationPopup;
    private string? _previousSceneId;
    private bool _confirmationDeletesScene;
    private readonly ConfirmationPopup _confirmationPopup;

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
        EditorProjectFileSaver projectFileSaver =
            new EditorProjectFileSaver(
                "assets/Project.sfp"
            );

        _projectSaver =
            new EditorProjectSaver(
                sceneSaver,
                projectFileSaver
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
        

        _sceneCreationPopup = 
            new SceneCreationPopup();

        _confirmationPopup =
            new ConfirmationPopup();

        _confirmationDeletesScene = false;

        
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
        string? currentSceneId =
            _state.CurrentDocument?.Scene.Id;

        _state.OpenScene(
            sceneId
        );

        if(currentSceneId != null &&
           currentSceneId != sceneId)
        {
            _previousSceneId =
                currentSceneId;
        }

        ResetSceneView();

        LoadCurrentSceneAssets();

        _toolbar.SetCurrentScene(
            sceneId
        );
    }

    private void CreateNewScene(
        string id,
        string name)
    {
        if(_state.Project == null)
            return;

        SceneDocument document =
            _state.Project.CreateScene(
                id,
                name
            );

        _state.OpenScene(
            document.Scene.Id
        );

        ResetSceneView();

        LoadCurrentSceneAssets();

        _toolbar.SetScenes(
            _state.Project.SceneOptions
        );

        _toolbar.SetCurrentScene(
            document.Scene.Id
        );

        return;
    }

    private void RenameCurrentScene(
        string name)
    {
        if(_state.Project == null)
            return;

        if(_state.CurrentDocument == null)
            return;

        _state.Project.RenameScene(
            _state.CurrentDocument.Scene.Id,
            name
        );

        _toolbar.SetScenes(
            _state.Project.SceneOptions
        );

        _toolbar.SetCurrentScene(
            _state.CurrentDocument.Scene.Id
        );
    }

    private void DeleteCurrentScene()
    {
        if(_state.Project == null)
            return;

        if(_state.CurrentDocument == null)
            return;

        string sceneId =
            _state.CurrentDocument.Scene.Id;

        string? previousSceneId =
            _previousSceneId;

        _state.Project.DeleteScene(
            sceneId
        );

        _toolbar.SetScenes(
            _state.Project.SceneOptions
        );

        if(previousSceneId != null &&
           _state.Project.SceneIds.Contains(previousSceneId))
        {
            _state.OpenScene(
                previousSceneId
            );

            ResetSceneView();

            LoadCurrentSceneAssets();

            _toolbar.SetCurrentScene(
                previousSceneId
            );

            _previousSceneId = null;

            return;
        }

        string fallbackSceneId =
            _state.Project.SceneIds.First();

        _state.OpenScene(
            fallbackSceneId
        );

        ResetSceneView();

        LoadCurrentSceneAssets();

        _toolbar.SetCurrentScene(
            fallbackSceneId
        );

        _previousSceneId = null;
    }

    private void ResetSceneView()
    {
        _contextMenu.Close();
        _inspector.Close();
    }


    private void ProcessSceneCreationPopup()
    {
        if(!_sceneCreationPopup.IsOpen)
            return;
    
        PopupResult result =
            _sceneCreationPopup.Update();
    
        if(result == PopupResult.Cancelled)
            return;
    
        if(result != PopupResult.Confirmed)
            return;
    
        try
        {
            if(_sceneCreationPopup.Mode == ScenePopupMode.Create)
            {
                CreateNewScene(
                    _sceneCreationPopup.SceneId,
                    _sceneCreationPopup.SceneName
                );
            }
            else if(_sceneCreationPopup.Mode == ScenePopupMode.Rename)
            {
                RenameCurrentScene(
                    _sceneCreationPopup.SceneName
                );
            }
    
            _sceneCreationPopup.Close();
        }
        catch(Exception exception)
        {
            _sceneCreationPopup.SetError(
                exception.Message
            );
        }
    }


    private void InitializeContent()
    {
        ProjectLoader projectLoader =
            new(
                "assets/Project.sfp",
                new ProjectSerializer()
            );

        ProjectData projectData =
            projectLoader.Load();
        
        EditorProjectLoader loader = new(
            "assets/scenes"
        );

        SceneProject project =
            loader.Load(
                projectData
            );

        EditorProject editorProject =
            new(
                projectData,
                project
            );
        
        
        
        foreach(SceneData scene in project.Scenes)
        {
            SceneDocument document =
                new(
                    scene,
                    $"{scene.Id}.json",
                    false
                );
                editorProject.AddDocument(
                    document
                );
        }

        editorProject.OpenDocument(
            editorProject.Data.StartupSceneId
        );        
        

        _state.OpenProject(
            editorProject
        );

        _inspector.SetProject(
            editorProject
        );

        _toolbar.SetScenes(
            editorProject.SceneOptions
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

    private void OpenNewScenePopup()
    {
        if(_state.Project == null)
            return;

        int number =
            _state.Project.SceneIds.Count + 1;

        _sceneCreationPopup.Open(
            $"scene_{number}",
            $"Scene {number}"
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

        _sceneCreationPopup.UpdateLayout(
            Raylib.GetScreenWidth(),
            Raylib.GetScreenHeight()
        );
        _confirmationPopup.UpdateLayout(
            Raylib.GetScreenWidth(),
            Raylib.GetScreenHeight()
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

        _sceneCreationPopup.UpdateLayout(
            width,
            height
        );
        _confirmationPopup.UpdateLayout(
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

        if(_sceneCreationPopup.IsOpen)
        {
            ProcessSceneCreationPopup();

            _input.EndFrame();

            return;
        }
        
        if(_confirmationPopup.IsOpen)
        {
            PopupResult result =
                _confirmationPopup.Update();

            if(result == PopupResult.Confirmed)
            {
                if(_confirmationDeletesScene)
                {
                    _confirmationDeletesScene = false;
                    try
                    {
                        DeleteCurrentScene();
                    }
                    catch(Exception exception)
                    {
                        Console.WriteLine(
                            $"Error deleting scene: {exception.Message}"
                        );
                    }
                }
            }

            _input.EndFrame();

            return;
        }


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

            case ToolbarAction.NewScene:
                OpenNewScenePopup();
                break;

            case ToolbarAction.RenameScene:
                if(_state.CurrentDocument != null)
                {
                    _sceneCreationPopup.OpenForRename(
                        _state.CurrentDocument.Scene.Name
                    );
                }

                break;

            case ToolbarAction.DeleteScene:
                if(_state.CurrentDocument != null)
                {
                    _confirmationDeletesScene = true;

                    _confirmationPopup.Open(
                        "Delete Scene",
                        $"Delete scene '{_state.CurrentDocument.Scene.Name}'?"
                    );
                }

                break;

            case ToolbarAction.SaveProject:
                SaveProject();
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
        
        _sceneCreationPopup.Draw();
        _confirmationPopup.Draw();

        Raylib.EndDrawing();
    }


    private void Shutdown()
    {
        _assetManager.Dispose();

        Raylib.CloseWindow();
    }
}