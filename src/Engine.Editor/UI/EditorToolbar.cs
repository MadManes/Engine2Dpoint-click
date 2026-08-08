using Raylib_cs;

namespace Engine.Editor.UI;

public class EditorToolbar
{
    private int _width;
    private readonly int _height;
    private readonly Rectangle _selectButton;
    private readonly Rectangle _moveButton;


    public EditorToolbar()
    {
        _height = 40;

        _width = 0;

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

    public void SetWidth(
        int width)
    {
        _width = width;
    }

    public void Draw()
    {
        Raylib.DrawRectangle(
            0,
            0,
            _width,
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
        System.Numerics.Vector2 mouse =
            Raylib.GetMousePosition();
    
    
        bool pressed =
            Raylib.IsMouseButtonPressed(
                MouseButton.Left
            );
    
    
        bool released =
            Raylib.IsMouseButtonReleased(
                MouseButton.Left
            );   
    
        if(!pressed)
        {
            return ToolbarAction.None;
        }
    
    
        if(Raylib.CheckCollisionPointRec(
            mouse,
            _selectButton))
        {
            Console.WriteLine("SELECT");
    
            return ToolbarAction.Select;
        }
    
    
        if(Raylib.CheckCollisionPointRec(
            mouse,
            _moveButton))
        {
            Console.WriteLine("MOVE");
    
            return ToolbarAction.Move;
        }
    
    
        return ToolbarAction.None;
    }
}