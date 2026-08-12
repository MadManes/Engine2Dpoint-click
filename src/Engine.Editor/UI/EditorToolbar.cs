using Raylib_cs;

namespace Engine.Editor.UI;

public class EditorToolbar
{
    private int _width;
    private bool _isDirty;

    private readonly int _height;

    
    private readonly Rectangle _entityButton;
    private readonly Rectangle _fileButton;
    private readonly Rectangle _backgroundButton;
    private readonly Rectangle _navigationButton;
    private readonly Rectangle _testButton;
        
    
    private readonly Rectangle _sceneField;
        
    
    private bool _fileDropdownOpen;
    private bool _backgroundDropdownOpen;
    private bool _entityDropdownOpen;    
    private bool _navigationDropdownOpen;
    private bool _testDropdownOpen;

    private bool _sceneDropdownOpen;

    private IReadOnlyCollection<string> _sceneIds =
        Array.Empty<string>();

    public string? RequestedSceneId { get; private set; }

    public EditorToolbar()
    {
        _height = 40;

        _width = 0;

        _isDirty = false;

        _fileButton = new Rectangle(
            10,
            5,
            60,
            30
        );

        _backgroundButton = new Rectangle(
            80,
            5,
            120,
            30
        );

        _entityButton = new Rectangle(
            210,
            5,
            80,
            30
        );

        _navigationButton = new Rectangle(
            300,
            5,
            120,
            30
        );

        _testButton = new Rectangle(
            430,
            5,
            70,
            30
        );

        _sceneField = new Rectangle(
            570,
            5,
            180,
            30
        );
        
        _fileDropdownOpen = false;
        _backgroundDropdownOpen = false;
        _entityDropdownOpen = false;
        _navigationDropdownOpen = false;
        _testDropdownOpen = false;
        
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

    private void CloseDropdowns()
    {
        _fileDropdownOpen = false;
        _backgroundDropdownOpen = false;
        _entityDropdownOpen = false;
        _navigationDropdownOpen = false;
        _testDropdownOpen = false;
    }

    private Rectangle GetFileOptionRectangle(
        int index)
    {
        return new Rectangle(
            _fileButton.X,
            _fileButton.Y +
                _fileButton.Height +
                index * 30,
            170,
            30
        );
    }    

    private Rectangle GetEntityOptionRectangle(
        int index)
    {
        return new Rectangle(
            _entityButton.X,
            _entityButton.Y +
                _entityButton.Height +
                index * 30,
            100,
            30
        );
    }


    private void DrawFileDropdown()
    {
        string[] options =
        {
            "New Project",
            "Load Project",
            "Save Project",
            "Save Project As...",
            "Exit"
        };

        for(int index = 0; index < options.Length; index++)
        {
            Rectangle option =
                GetFileOptionRectangle(index);

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
                options[index],
                (int)option.X + 8,
                (int)option.Y + 6,
                18,
                Color.White
            );
        }
    }

    private void DrawEntityDropdown()
    {
        string[] options =
        {
            "Select",
            "Move",
            "Create"
        };

        for(int index = 0; index < options.Length; index++)
        {
            Rectangle option =
                GetEntityOptionRectangle(index);

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
                options[index],
                (int)option.X + 8,
                (int)option.Y + 6,
                18,
                Color.White
            );
        }
    }

    private void DrawBackgroundDropdown()
    {
        Rectangle option = new Rectangle(
            _backgroundButton.X,
            _backgroundButton.Y +
                _backgroundButton.Height,
            190,
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
            "Load Background",
            (int)option.X + 8,
            (int)option.Y + 6,
            18,
            Color.White
        );
    }

    private void DrawNavigationDropdown()
    {
        string[] options =
        {
            "Walkable Areas",
            "Vertices",
            "Edges",
            "Pathfinding"
        };

        for(int index = 0; index < options.Length; index++)
        {
            Rectangle option = new Rectangle(
                _navigationButton.X,
                _navigationButton.Y +
                    _navigationButton.Height +
                    index * 30,
                180,
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
                options[index],
                (int)option.X + 8,
                (int)option.Y + 6,
                18,
                Color.White
            );
        }
    }

    private void DrawTestDropdown()
    {
        Rectangle option = new Rectangle(
            _testButton.X,
            _testButton.Y +
                _testButton.Height,
            120,
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
            "Play",
            (int)option.X + 8,
            (int)option.Y + 6,
            18,
            Color.White
        );
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
            _fileButton,
            Color.Gray
        );

        Raylib.DrawRectangleRec(
            _backgroundButton,
            Color.Gray
        );

        Raylib.DrawRectangleRec(
            _entityButton,
            Color.Gray
        );

        Raylib.DrawRectangleRec(
            _navigationButton,
            Color.Gray
        );

        Raylib.DrawRectangleRec(
            _testButton,
            Color.Gray
        );

        Raylib.DrawText(
            "File",
            18,
            12,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Background",
            88,
            12,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Entity",
            218,
            12,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Navigation",
            308,
            12,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Test",
            448,
            12,
            18,
            Color.White
        );    

        Color statusColor =
            _isDirty
                ? Color.Red
                : Color.Green;
        
        Raylib.DrawCircle(
            970,
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

        if(_fileDropdownOpen)
        {
            DrawFileDropdown();
        }

        if(_entityDropdownOpen)
        {
            DrawEntityDropdown();
        }

        if(_backgroundDropdownOpen)
        {
            DrawBackgroundDropdown();
        }

        if(_navigationDropdownOpen)
        {
            DrawNavigationDropdown();
        }

        if(_testDropdownOpen)
        {
            DrawTestDropdown();
        }

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

        if(pressed && _fileDropdownOpen)
        {
            for(int index = 0; index < 5; index++)
            {
                Rectangle option =
                    GetFileOptionRectangle(index);

                if(Raylib.CheckCollisionPointRec(
                    mouse,
                    option))
                {
                    _fileDropdownOpen = false;

                    if(index == 2)
                    {
                        return ToolbarAction.SaveProject;
                    }

                    return ToolbarAction.None;
                }
            }

            _fileDropdownOpen = false;

            return ToolbarAction.None;
        }

        if(pressed && _entityDropdownOpen)
        {
            Rectangle selectOption =
                GetEntityOptionRectangle(0);

            Rectangle moveOption =
                GetEntityOptionRectangle(1);

            Rectangle createOption =
                GetEntityOptionRectangle(2);

            if(Raylib.CheckCollisionPointRec(
                mouse,
                selectOption))
            {
                _entityDropdownOpen = false;

                return ToolbarAction.Select;
            }

            if(Raylib.CheckCollisionPointRec(
                mouse,
                moveOption))
            {
                _entityDropdownOpen = false;

                return ToolbarAction.Move;
            }

            if(Raylib.CheckCollisionPointRec(
                mouse,
                createOption))
            {
                _entityDropdownOpen = false;

                return ToolbarAction.Create;
            }

            _entityDropdownOpen = false;

            return ToolbarAction.None;
        }
    
    
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
            _fileButton))
        {
            bool wasOpen = _fileDropdownOpen;

            CloseDropdowns();

            _fileDropdownOpen = !wasOpen;

            _sceneDropdownOpen = false;

            return ToolbarAction.None;
        }

        if(Raylib.CheckCollisionPointRec(
            mouse,
            _backgroundButton))
        {
            bool wasOpen = _backgroundDropdownOpen;

            CloseDropdowns();

            _backgroundDropdownOpen = !wasOpen;

            _sceneDropdownOpen = false;

            return ToolbarAction.None;
        }

        if(Raylib.CheckCollisionPointRec(
            mouse,
            _entityButton))
        {
            bool wasOpen = _entityDropdownOpen;

            CloseDropdowns();

            _entityDropdownOpen = !wasOpen;

            _sceneDropdownOpen = false;
    
            return ToolbarAction.None;
        }

        if(Raylib.CheckCollisionPointRec(
            mouse,
            _navigationButton))
        {
            bool wasOpen = _navigationDropdownOpen;

            CloseDropdowns();

            _navigationDropdownOpen = !wasOpen;

            _sceneDropdownOpen = false;

            return ToolbarAction.None;
        }

        if(Raylib.CheckCollisionPointRec(
            mouse,
            _testButton))
        {
            bool wasOpen = _testDropdownOpen;

            CloseDropdowns();

            _testDropdownOpen = !wasOpen;

            _sceneDropdownOpen = false;

            return ToolbarAction.None;
        }
        

        
        if(Raylib.CheckCollisionPointRec(
            mouse,
            _sceneField))
        {
            _sceneDropdownOpen =
                !_sceneDropdownOpen;
            
            _entityDropdownOpen = false;

            return ToolbarAction.None;
        }
    
    
        return ToolbarAction.None;
    }
}