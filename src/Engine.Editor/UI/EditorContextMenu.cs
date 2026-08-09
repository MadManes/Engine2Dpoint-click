using Engine.Scene.Definitions;
using Raylib_cs;

namespace Engine.Editor.UI;

public class EditorContextMenu
{
    private const int Width = 140;
    private const int Height = 90;

    private Rectangle _bounds;

    public bool Visible { get; private set; }

    public EntityData? Target { get; private set; }


    public void Open(
        EntityData entity,
        int x,
        int y)
    {
        Target = entity;

        _bounds = new Rectangle(
            x,
            y,
            Width,
            Height
        );

        Visible = true;
    }


    public void Close()
    {
        Target = null;
        Visible = false;
    }

    public ContextMenuAction Update()
    {
        if(!Visible)
            return ContextMenuAction.None;

        if(!Raylib.IsMouseButtonPressed(
            MouseButton.Left))
        {
            return ContextMenuAction.None;
        }

        System.Numerics.Vector2 mouse =
            Raylib.GetMousePosition();


        Rectangle editButton =
            new Rectangle(
                _bounds.X,
                _bounds.Y,
                _bounds.Width,
                30
            );


        Rectangle deleteButton =
            new Rectangle(
                _bounds.X,
                _bounds.Y + 30,
                _bounds.Width,
                30
            );


        Rectangle duplicateButton =
            new Rectangle(
                _bounds.X,
                _bounds.Y + 60,
                _bounds.Width,
                30
            );


        if(Raylib.CheckCollisionPointRec(
            mouse,
            editButton))
        {
            return ContextMenuAction.Edit;
        }


        if(Raylib.CheckCollisionPointRec(
            mouse,
            deleteButton))
        {
            return ContextMenuAction.Delete;
        }


        if(Raylib.CheckCollisionPointRec(
            mouse,
            duplicateButton))
        {
            return ContextMenuAction.Duplicate;
        }


        Close();

        return ContextMenuAction.None;
    }

    public void Draw()
    {
        if(!Visible)
            return;

        Raylib.DrawRectangleRec(
            _bounds,
            Color.DarkGray
        );

        Raylib.DrawRectangleLinesEx(
            _bounds,
            1,
            Color.White
        );

        Raylib.DrawText(
            "Edit",
            (int)_bounds.X + 10,
            (int)_bounds.Y + 10,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Delete",
            (int)_bounds.X + 10,
            (int)_bounds.Y + 35,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Duplicate",
            (int)_bounds.X + 10,
            (int)_bounds.Y + 60,
            18,
            Color.White
        );
    }
}