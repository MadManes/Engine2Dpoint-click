using Engine.Editor.State;
using Engine.Editor.Loading;
using Engine.Scene.Definitions;
using Engine.Editor.Documents;
using Raylib_cs;

namespace Engine.Editor;

public class Editor
{
    private readonly EditorState _state;    

    public Editor()
    {
        _state = new EditorState();

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
    }


    public void Run()
    {
        Initialize();


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

    }


    private void Render()
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(
            Color.Black
        );


        Raylib.DrawText(
            "Engine Editor",
            20,
            20,
            30,
            Color.White
        );
        
        if (_state.CurrentDocument != null)
        {
            Raylib.DrawText(
                $"Editing: {_state.CurrentDocument.Scene.Id}",
                20,
                60,
                20,
                Color.White
            );
        }

        Raylib.EndDrawing();
    }


    private void Shutdown()
    {
        Raylib.CloseWindow();
    }
}