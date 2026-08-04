using Engine.Editor.State;
using Raylib_cs;

namespace Engine.Editor;

public class Editor
{
    private readonly EditorState _state;


    public Editor()
    {
        _state = new EditorState();
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


        Raylib.EndDrawing();
    }


    private void Shutdown()
    {
        Raylib.CloseWindow();
    }
}