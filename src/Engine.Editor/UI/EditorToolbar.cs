using Raylib_cs;

namespace Engine.Editor.UI;

public class EditorToolbar
{
    private readonly int _height;
    private readonly Rectangle _selectButton;
    private readonly Rectangle _moveButton;


    public EditorToolbar()
    {
        _height = 40;

        _selectButton = new Rectangle(
            10,
            5,
            70,
            30
        );
        
        _moveButton = new Rectangle(
            90,
            5,
            70,
            30
        );
    }


    public void Draw()
    {
        Raylib.DrawRectangle(
            0,
            0,
            1280,
            _height,
            Color.DarkGray
        );

        Raylib.DrawRectangleRec(
            _selectButton,
            Color.Gray
        );

        Raylib.DrawRectangleRec(
            _moveButton,
            Color.Gray
        );


        Raylib.DrawText(
            "Select",
            20,
            12,
            18,
            Color.White
        );


        Raylib.DrawText(
            "Move",
            100,
            12,
            18,
            Color.White
        );
    }

    public ToolbarAction Update()
    {
        if(!Raylib.IsMouseButtonPressed(
            MouseButton.Left))
        {
            return ToolbarAction.None;
        }
    
        System.Numerics.Vector2 mouse =
            Raylib.GetMousePosition();
    
        if(Raylib.CheckCollisionPointRec(
            mouse,
            _selectButton))
        {
            return ToolbarAction.Select;
        }
    
        if(Raylib.CheckCollisionPointRec(
            mouse,
            _moveButton))
        {
            return ToolbarAction.Move;
        }
    
        return ToolbarAction.None;
    }
}