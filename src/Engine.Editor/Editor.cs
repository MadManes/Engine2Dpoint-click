using Engine.Math;
using Engine.Assets;
using Engine.Scene.Definitions;
using Engine.Editor.Input;
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

        Vector2 mouseWorld =
            _input.MouseWorldPosition();


        if(_input.LeftPressed())
        {
            EntityData? selected =
                _entityPicker.Pick(
                    _state.CurrentDocument!.Scene.EntityCollection.Entities,
                    mouseWorld
                );


            _state.SelectEntity(selected);


            if(selected != null)
            {
                Vector2 offset =
                    new Vector2(
                        selected.Position.X - mouseWorld.X,
                        selected.Position.Y - mouseWorld.Y
                    );


                _state.StartDragging(offset);
            }
        }


        if(_input.LeftDown()
            &&
            _state.IsDragging
            &&
            _state.SelectedEntity != null)
        {
            EntityData entity =
                _state.SelectedEntity;


            entity.Position =
                new Vector2(
                    mouseWorld.X + _state.DragOffset.X,
                    mouseWorld.Y + _state.DragOffset.Y
                );


            _state.CurrentDocument!
                .MarkDirty();

        }


        if(_input.LeftReleased())
        {
            _state.StopDragging();
        }
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