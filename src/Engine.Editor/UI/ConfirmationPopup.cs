using Raylib_cs;

namespace Engine.Editor.UI;

public class ConfirmationPopup
{
    private bool _isOpen;

    private int _screenWidth;
    private int _screenHeight;

    private Rectangle _window;
    private Rectangle _cancelButton;
    private Rectangle _confirmButton;

    private string _title;
    private string _message;
    private bool _showCancelButton;

    public bool IsOpen =>
        _isOpen;

    public ConfirmationPopup()
    {
        _isOpen = false;

        _screenWidth = 0;
        _screenHeight = 0;

        _title = "";
        _message = "";
        _showCancelButton = true;
    }

    public void UpdateLayout(
        int screenWidth,
        int screenHeight)
    {
        _screenWidth = screenWidth;
        _screenHeight = screenHeight;

        const int width = 500;
        const int height = 220;

        _window = new Rectangle(
            (screenWidth - width) / 2,
            (screenHeight - height) / 2,
            width,
            height
        );

        _cancelButton = new Rectangle(
            _window.X + 250,
            _window.Y + 160,
            100,
            35
        );

        _confirmButton = new Rectangle(
            _window.X + 360,
            _window.Y + 160,
            100,
            35
        );
    }

    public void Open(
        string title,
        string message,
        bool showCancelButton = true)
    {
        _title = title;
        _message = message;
        _showCancelButton = showCancelButton;

        _isOpen = true;
    }

    public void Close()
    {
        _isOpen = false;
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
            if(_showCancelButton &&
                Raylib.CheckCollisionPointRec(
                mouse,
                _cancelButton))
            {
                Close();

                return PopupResult.Cancelled;
            }

            if(Raylib.CheckCollisionPointRec(
                mouse,
                _confirmButton))
            {
                Close();

                return PopupResult.Confirmed;
            }
        }

        return PopupResult.None;
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
            _title,
            (int)_window.X + 30,
            (int)_window.Y + 20,
            24,
            Color.White
        );

        Raylib.DrawText(
            _message,
            (int)_window.X + 30,
            (int)_window.Y + 75,
            18,
            Color.White
        );

        if(_showCancelButton)
        {
            DrawButton(
                _cancelButton,
                "Cancel"
            );
        }

        DrawButton(
            _confirmButton,
            "Confirm"
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
            (int)button.X + 18,
            (int)button.Y + 8,
            18,
            Color.White
        );
    }
}