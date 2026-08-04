using Engine.Editor.State;
using Engine.Editor.Loading;
using Engine.Scene.Definitions;
using Raylib_cs;

namespace Engine.Editor;

public class Editor
{
    private readonly EditorState _state;
    private readonly SceneProject _project;

    public Editor()
    {
        _state = new EditorState();

        EditorProjectLoader loader = new(
            "assets/scenes"
        );

        _project = loader.Load();
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

        Raylib.DrawText(
            $"Scenes: {_project.Scenes.Count}",
            20,
            90,
            30,
            Color.White
        );

        // Listar escenas
        int y = 125;

        foreach(SceneData scene in _project.Scenes)
        {
            Raylib.DrawText(
                scene.Id,
                20,
                y,
                20,
                Color.White
            );

            y += 25;
        }

        Raylib.EndDrawing();
    }


    private void Shutdown()
    {
        Raylib.CloseWindow();
    }
}