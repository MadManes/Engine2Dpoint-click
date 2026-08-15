using Raylib_cs;

namespace Engine.Editor.UI;

public class EditorPopup
{
    private bool _isOpen;

    private int _screenWidth;
    private int _screenHeight;

    private Rectangle _window;
    private Rectangle _inputField;
    private Rectangle _cancelButton;
    private Rectangle _confirmButton;

    private string _title;
    private string _text;

    private bool _textFocused;

    public bool IsOpen =>
        _isOpen;

    public string Text =>
        _text;

    public EditorPopup()
    {
        _isOpen = false;

        _screenWidth = 0;
        _screenHeight = 0;

        _window = new Rectangle();
        _inputField = new Rectangle();
        _cancelButton = new Rectangle();
        _confirmButton = new Rectangle();

        _title = "";
        _text = "";

        _textFocused = false;
    }

    public void UpdateLayout(
        int screenWidth,
        int screenHeight)
    {
        _screenWidth = screenWidth;
        _screenHeight = screenHeight;

        const int width = 500;
        const int height = 250;

        _window = new Rectangle(
            (screenWidth - width) / 2,
            (screenHeight - height) / 2,
            width,
            height
        );

        _inputField = new Rectangle(
            _window.X + 30,
            _window.Y + 90,
            _window.Width - 60,
            40
        );

        _cancelButton = new Rectangle(
            _window.X + 250,
            _window.Y + 180,
            100,
            35
        );

        _confirmButton = new Rectangle(
            _window.X + 360,
            _window.Y + 180,
            100,
            35
        );
    }

    public void Open(
        string title,
        string initialText)
    {
        _title = title;
        _text = initialText;

        _textFocused = true;

        _isOpen = true;
    }

    public void Close()
    {
        _isOpen = false;
        _textFocused = false;
    }

    public PopupResult Update()
    {
        if(!_isOpen)
            return PopupResult.None;

        if(Raylib.IsKeyPressed(
            KeyboardKey.Escape))
        {
            Close();

            return PopupResult.Cancelled;
        }

        if(Raylib.CheckCollisionPointRec(
            Raylib.GetMousePosition(),
            _inputField))
        {
            if(Raylib.IsMouseButtonPressed(
                MouseButton.Left))
            {
                _textFocused = true;
            }
        }

        if(Raylib.IsMouseButtonPressed(
            MouseButton.Left))
        {
            System.Numerics.Vector2 mouse =
                Raylib.GetMousePosition();

            if(Raylib.CheckCollisionPointRec(
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

        if(_textFocused)
        {
            int key =
                Raylib.GetCharPressed();

            while(key > 0)
            {
                if(key >= 32 &&
                   key <= 125)
                {
                    _text += (char)key;
                }

                key = Raylib.GetCharPressed();
            }

            if(Raylib.IsKeyPressed(
                KeyboardKey.Backspace))
            {
                if(_text.Length > 0)
                {
                    _text =
                        _text.Substring(
                            0,
                            _text.Length - 1
                        );
                }
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
            "Name",
            (int)_inputField.X,
            (int)_inputField.Y - 25,
            18,
            Color.White
        );

        Raylib.DrawRectangleRec(
            _inputField,
            Color.White
        );

        Raylib.DrawText(
            _text,
            (int)_inputField.X + 8,
            (int)_inputField.Y + 9,
            18,
            Color.Black
        );

        Raylib.DrawRectangleRec(
            _cancelButton,
            Color.Gray
        );

        Raylib.DrawRectangleLines(
            (int)_cancelButton.X,
            (int)_cancelButton.Y,
            (int)_cancelButton.Width,
            (int)_cancelButton.Height,
            Color.White
        );

        Raylib.DrawText(
            "Cancel",
            (int)_cancelButton.X + 20,
            (int)_cancelButton.Y + 8,
            18,
            Color.White
        );

        Raylib.DrawRectangleRec(
            _confirmButton,
            Color.Gray
        );

        Raylib.DrawRectangleLines(
            (int)_confirmButton.X,
            (int)_confirmButton.Y,
            (int)_confirmButton.Width,
            (int)_confirmButton.Height,
            Color.White
        );

        Raylib.DrawText(
            "OK",
            (int)_confirmButton.X + 35,
            (int)_confirmButton.Y + 8,
            18,
            Color.White
        );
    }
}