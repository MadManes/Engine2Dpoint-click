using Raylib_cs;

namespace Engine.Editor.UI;

public class EditorToolbar
{
    private int _width;
    private bool _isDirty;

    private readonly int _height;

    
    private readonly Rectangle _entityButton;
    private readonly Rectangle _fileButton;
    private readonly Rectangle _sceneButton;
    private readonly Rectangle _backgroundButton;
    private readonly Rectangle _navigationButton;
    private readonly Rectangle _testButton;
        
    
    private readonly Rectangle _sceneField;
        
    
    private bool _fileDropdownOpen;
    private bool _sceneMenuOpen;
    private bool _backgroundDropdownOpen;
    private bool _entityDropdownOpen;    
    private bool _navigationDropdownOpen;
    private bool _testDropdownOpen;

    private bool _sceneSelectorOpen;    
    
    private IReadOnlyCollection<EditorSceneOption> _scenes =
        Array.Empty<EditorSceneOption>();

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

        _sceneButton = new Rectangle(
            80,
            5,
            80,
            30
        );

        _backgroundButton = new Rectangle(
            170,
            5,
            120,
            30
        );

        _entityButton = new Rectangle(
            300,
            5,
            80,
            30
        );

        _navigationButton = new Rectangle(
            390,
            5,
            120,
            30
        );

        _testButton = new Rectangle(
            520,
            5,
            70,
            30
        );

        _sceneField = new Rectangle(
            610,
            5,
            180,
            30
        );
        
        _fileDropdownOpen = false;
        _sceneMenuOpen = false;
        _backgroundDropdownOpen = false;
        _entityDropdownOpen = false;
        _navigationDropdownOpen = false;
        _testDropdownOpen = false;
        
        _sceneSelectorOpen = false;        
        
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
        IReadOnlyCollection<EditorSceneOption> scenes)
    {        
        _scenes = scenes;
    }

    public void SetCurrentScene(
        string sceneId)
    {
        RequestedSceneId = sceneId;
    }

    private string GetCurrentSceneName()
    {
        if(RequestedSceneId == null)
            return "Scene";

        EditorSceneOption? scene =
            _scenes.FirstOrDefault(
                x => x.Id == RequestedSceneId
            );

        return scene?.Name ?? RequestedSceneId;
    }

    private void CloseDropdowns()
    {
        _fileDropdownOpen = false;
        _sceneMenuOpen = false;
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

    private void DrawSceneMenu()
    {
        Rectangle option = new Rectangle(
            _sceneButton.X,
            _sceneButton.Y +
                _sceneButton.Height,
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
            "New Scene",
            (int)option.X + 8,
            (int)option.Y + 6,
            18,
            Color.White
        );
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

        foreach(EditorSceneOption scene in _scenes)
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
                scene.Name,
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
            _sceneButton,
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
            "Scene",
            88,
            12,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Background",
            178,
            12,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Entity",
            308,
            12,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Navigation",
            398,
            12,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Test",
            528,
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
            GetCurrentSceneName(),
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

        if(_sceneMenuOpen)
        {
            DrawSceneMenu();
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

        if(_sceneSelectorOpen)
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

        if(pressed && _sceneMenuOpen)
        {
            Rectangle option = new Rectangle(
                _sceneButton.X,
                _sceneButton.Y +
                    _sceneButton.Height,
                120,
                30
            );

            if(Raylib.CheckCollisionPointRec(
                mouse,
                option))
            {
                _sceneMenuOpen = false;

                return ToolbarAction.NewScene;
            }

            _sceneMenuOpen = false;

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
    
    
        if(pressed && _sceneSelectorOpen)
        {
            int index = 0;

            foreach(EditorSceneOption scene in _scenes)
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
                    RequestedSceneId = scene.Id;

                    _sceneSelectorOpen = false;

                    return ToolbarAction.Scene;
                }

                index++;
            }

            _sceneSelectorOpen = false;

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

            _sceneSelectorOpen = false;

            return ToolbarAction.None;
        }

        if(Raylib.CheckCollisionPointRec(
            mouse,
            _sceneButton))
        {
            bool wasOpen = _sceneMenuOpen;

            CloseDropdowns();

            _sceneMenuOpen = !wasOpen;

            _sceneSelectorOpen = false;

            return ToolbarAction.None;
        }

        if(Raylib.CheckCollisionPointRec(
            mouse,
            _backgroundButton))
        {
            bool wasOpen = _backgroundDropdownOpen;

            CloseDropdowns();

            _backgroundDropdownOpen = !wasOpen;

            _sceneSelectorOpen = false;

            return ToolbarAction.None;
        }

        if(Raylib.CheckCollisionPointRec(
            mouse,
            _entityButton))
        {
            bool wasOpen = _entityDropdownOpen;

            CloseDropdowns();

            _entityDropdownOpen = !wasOpen;

            _sceneSelectorOpen = false;
    
            return ToolbarAction.None;
        }

        if(Raylib.CheckCollisionPointRec(
            mouse,
            _navigationButton))
        {
            bool wasOpen = _navigationDropdownOpen;

            CloseDropdowns();

            _navigationDropdownOpen = !wasOpen;

            _sceneSelectorOpen = false;

            return ToolbarAction.None;
        }

        if(Raylib.CheckCollisionPointRec(
            mouse,
            _testButton))
        {
            bool wasOpen = _testDropdownOpen;

            CloseDropdowns();

            _testDropdownOpen = !wasOpen;

            _sceneSelectorOpen = false;

            return ToolbarAction.None;
        }
        

        
        if(Raylib.CheckCollisionPointRec(
            mouse,
            _sceneField))
        {
            bool wasOpen = _sceneSelectorOpen;

            CloseDropdowns();

            _sceneSelectorOpen = !wasOpen;            

            return ToolbarAction.None;
        }
    
    
        return ToolbarAction.None;
    }
}