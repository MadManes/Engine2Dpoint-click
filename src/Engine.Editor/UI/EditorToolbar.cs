using Raylib_cs;

namespace Engine.Editor.UI;

public class EditorToolbar
{
    private int _width;
    private bool _isDirty;

    private readonly int _height;
    private readonly Rectangle _selectButton;
    private readonly Rectangle _moveButton;
    private readonly Rectangle _createButton;
    
    private readonly Rectangle _sceneField;
    private bool _sceneDropdownOpen;

    private IReadOnlyCollection<string> _sceneIds =
        Array.Empty<string>();

    public string? RequestedSceneId { get; private set; }

    public EditorToolbar()
    {
        _height = 40;

        _width = 0;

        _isDirty = false;

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

        _createButton = new Rectangle(
            170,
            5,
            70,
            30
        );

        _sceneField = new Rectangle(
            300,
            5,
            180,
            30
        );

        _sceneDropdownOpen = false;
    }

    public void SetWidth(
        int width)
    {
        _width = width;
    }

    public void SetDocumentState(
    bool isDirty)
    {
        _isDirty = isDirty;
    }

    public void SetScenes(
        IReadOnlyCollection<string> sceneIds)
    {
        _sceneIds = sceneIds;
    }

    public void SetCurrentScene(
        string sceneId)
    {
        RequestedSceneId = sceneId;
    }


    private void DrawSceneDropdown()
    {
        int index = 0;

        foreach(string sceneId in _sceneIds)
        {
            Rectangle option = new Rectangle(
                _sceneField.X,
                _sceneField.Y +
                    _sceneField.Height +
                    index * 30,
                _sceneField.Width,
                30
            );

            Raylib.DrawRectangleRec(
                option,
                Color.DarkGray
            );

            Raylib.DrawRectangleLines(
                (int)option.X,
                (int)option.Y,
                (int)option.Width,
                (int)option.Height,
                Color.White
            );

            Raylib.DrawText(
                sceneId,
                (int)option.X + 8,
                (int)option.Y + 6,
                18,
                Color.White
            );

            index++;
        }
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

        Raylib.DrawRectangleRec(
            _createButton,
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

        Raylib.DrawText(
            "Create",
            177,
            12,
            18,
            Color.White
        );

        Color statusColor =
            _isDirty
                ? Color.Red
                : Color.Green;
        
        Raylib.DrawCircle(
            270,
            20,
            7,
            statusColor
        );

        Raylib.DrawRectangleRec(
            _sceneField,
            Color.Gray
        );

        Raylib.DrawText(
            RequestedSceneId ?? "Scene",
            (int)_sceneField.X + 8,
            (int)_sceneField.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "▼",
            (int)_sceneField.X + 155,
            (int)_sceneField.Y + 6,
            18,
            Color.White
        );

        if(_sceneDropdownOpen)
        {
            DrawSceneDropdown();
        }
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
    
    
        if(pressed && _sceneDropdownOpen)
        {
            int index = 0;

            foreach(string sceneId in _sceneIds)
            {
                Rectangle option = new Rectangle(
                    _sceneField.X,
                    _sceneField.Y +
                        _sceneField.Height +
                        index * 30,
                    _sceneField.Width,
                    30
                );

                if(Raylib.CheckCollisionPointRec(
                    mouse,
                    option))
                {
                    RequestedSceneId = sceneId;

                    _sceneDropdownOpen = false;

                    return ToolbarAction.Scene;
                }

                index++;
            }

            _sceneDropdownOpen = false;

            return ToolbarAction.None;
        }


        if(!pressed)
        {
            return ToolbarAction.None;
        }
    
        if(Raylib.CheckCollisionPointRec(
            mouse,
            _sceneField))
        {
            _sceneDropdownOpen =
                !_sceneDropdownOpen;

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

        if(Raylib.CheckCollisionPointRec(
            mouse,
            _createButton))
        {
            Console.WriteLine("CREATE");
    
            return ToolbarAction.Create;
        }
    
    
        return ToolbarAction.None;
    }
}