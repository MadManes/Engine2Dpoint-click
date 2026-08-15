using Raylib_cs;

namespace Engine.Editor.UI;

public class SceneCreationPopup
{
    private bool _isOpen;

    private int _screenWidth;
    private int _screenHeight;

    private Rectangle _window;

    private Rectangle _idField;
    private Rectangle _nameField;

    private Rectangle _cancelButton;
    private Rectangle _confirmButton;

    private string _sceneId;
    private string _sceneName;

    private bool _idFocused;
    private bool _nameFocused;

    private string _errorMessage;

    public bool IsOpen =>
        _isOpen;

    public string SceneId =>
        _sceneId;

    public string SceneName =>
        _sceneName;

    public SceneCreationPopup()
    {
        _sceneId = "";
        _sceneName = "";

        _errorMessage = "";

        _isOpen = false;
    }

    public void UpdateLayout(
        int screenWidth,
        int screenHeight)
    {
        _screenWidth = screenWidth;
        _screenHeight = screenHeight;

        const int width = 500;
        const int height = 340;

        _window = new Rectangle(
            (screenWidth - width) / 2,
            (screenHeight - height) / 2,
            width,
            height
        );

        _idField = new Rectangle(
            _window.X + 30,
            _window.Y + 80,
            _window.Width - 60,
            40
        );

        _nameField = new Rectangle(
            _window.X + 30,
            _window.Y + 150,
            _window.Width - 60,
            40
        );

        _cancelButton = new Rectangle(
            _window.X + 250,
            _window.Y + 280,
            100,
            35
        );

        _confirmButton = new Rectangle(
            _window.X + 360,
            _window.Y + 280,
            100,
            35
        );
    }

    public void SetError(
        string message)
    {
        _errorMessage = message;
    }

    public void Open(
        string sceneId,
        string sceneName)
    {
        _sceneId = sceneId;
        _sceneName = sceneName;

        _errorMessage = "";

        _idFocused = true;
        _nameFocused = false;

        _isOpen = true;
    }

    public void Close()
    {
        _isOpen = false;

        _idFocused = false;
        _nameFocused = false;

        _errorMessage = "";
    }

    public PopupResult Update()
    {
        if(!_isOpen)
            return PopupResult.None;

        System.Numerics.Vector2 mouse =
            Raylib.GetMousePosition();

        if(Raylib.IsKeyPressed(
            KeyboardKey.Escape))
        {
            Close();

            return PopupResult.Cancelled;
        }

        if(Raylib.IsMouseButtonPressed(
            MouseButton.Left))
        {
            if(Raylib.CheckCollisionPointRec(
                mouse,
                _idField))
            {
                _idFocused = true;
                _nameFocused = false;
            }
            else if(Raylib.CheckCollisionPointRec(
                mouse,
                _nameField))
            {
                _idFocused = false;
                _nameFocused = true;
            }
            else if(Raylib.CheckCollisionPointRec(
                mouse,
                _cancelButton))
            {
                Close();

                return PopupResult.Cancelled;
            }
            else if(Raylib.CheckCollisionPointRec(
                mouse,
                _confirmButton))
            {
                return PopupResult.Confirmed;
            }
        }

        if(_idFocused)
        {
            ReadText(
                ref _sceneId
            );
        }

        if(_nameFocused)
        {
            ReadText(
                ref _sceneName
            );
        }

        return PopupResult.None;
    }

    private void ReadText(
        ref string text)
    {
        int key =
            Raylib.GetCharPressed();

        while(key > 0)
        {
            if(key >= 32 &&
               key <= 125)
            {
                text += (char)key;
            }

            key = Raylib.GetCharPressed();
        }

        if(Raylib.IsKeyPressed(
            KeyboardKey.Backspace))
        {
            if(text.Length > 0)
            {
                text =
                    text.Substring(
                        0,
                        text.Length - 1
                    );
            }
        }
    }

    public void Draw()
    {
        if(!_isOpen)
            return;

        Raylib.DrawRectangle(
            0,
            0,
            _screenWidth,
            _screenHeight,
            new Color(
                0,
                0,
                0,
                150
            )
        );

        Raylib.DrawRectangleRec(
            _window,
            Color.DarkGray
        );

        Raylib.DrawRectangleLines(
            (int)_window.X,
            (int)_window.Y,
            (int)_window.Width,
            (int)_window.Height,
            Color.White
        );

        Raylib.DrawText(
            "New Scene",
            (int)_window.X + 30,
            (int)_window.Y + 20,
            24,
            Color.White
        );

        DrawField(
            _idField,
            "Scene ID",
            _sceneId
        );

        DrawField(
            _nameField,
            "Scene Name",
            _sceneName
        );

        if(!string.IsNullOrWhiteSpace(_errorMessage))
        {
            Raylib.DrawText(
                _errorMessage,
                (int)_window.X + 30,
                (int)_window.Y + 200,
                16,
                Color.Red
            );
        }

        DrawButton(
            _cancelButton,
            "Cancel"
        );

        DrawButton(
            _confirmButton,
            "Create"
        );
    }

    private void DrawField(
        Rectangle field,
        string label,
        string text)
    {
        Raylib.DrawText(
            label,
            (int)field.X,
            (int)field.Y - 23,
            18,
            Color.White
        );

        Raylib.DrawRectangleRec(
            field,
            Color.White
        );

        Raylib.DrawText(
            text,
            (int)field.X + 8,
            (int)field.Y + 9,
            18,
            Color.Black
        );
    }

    private void DrawButton(
        Rectangle button,
        string text)
    {
        Raylib.DrawRectangleRec(
            button,
            Color.Gray
        );

        Raylib.DrawRectangleLines(
            (int)button.X,
            (int)button.Y,
            (int)button.Width,
            (int)button.Height,
            Color.White
        );

        Raylib.DrawText(
            text,
            (int)button.X + 20,
            (int)button.Y + 8,
            18,
            Color.White
        );
    }
}