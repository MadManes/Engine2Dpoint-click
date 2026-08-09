using Raylib_cs;
using Engine.Entities.Components;
using Engine.Scene.Definitions;

namespace Engine.Editor.UI;

public class EditorInspector
{
    private const int Width = 260;

    private bool _editingName;

    private bool _typeDropdownOpen;

    private string _nameBuffer = "";

    public bool Visible { get; private set; }

    public EntityData? Target { get; private set; }

    private readonly InspectorLayout _layout;


    public EditorInspector()
    {
        _layout = new InspectorLayout();
    }
    

    public void Open(
        EntityData entity)
    {
        Target = entity;
        _nameBuffer = entity.Name;

        _editingName = false;
        _typeDropdownOpen = false;

        Visible = true;
    }


    public void Close()
    {
        Target = null;
        Visible = false;
    }

    private void DrawTypeDropdown()
    {
        Rectangle generic =
            _layout.TypeGenericOption;

        Rectangle player =
            _layout.TypePlayerOption;


        int bottom =
            (int)player.Y +
            (int)player.Height;


        Raylib.DrawRectangle(
            (int)generic.X,
            (int)generic.Y,
            (int)generic.Width,
            bottom - (int)generic.Y,
            Color.DarkGray
        );


        Raylib.DrawRectangleLines(
            (int)generic.X,
            (int)generic.Y,
            (int)generic.Width,
            bottom - (int)generic.Y,
            Color.White
        );


        Raylib.DrawText(
            "Generic",
            (int)generic.X + 8,
            (int)generic.Y + 6,
            18,
            Color.White
        );


        Raylib.DrawText(
            "Player",
            (int)player.X + 8,
            (int)player.Y + 6,
            18,
            Color.White
        );
    }

    public InspectorResult Update()
    {
        if(!Visible || Target == null)
            return InspectorResult.None;


        if(Raylib.IsMouseButtonPressed(
            MouseButton.Left))
        {
            System.Numerics.Vector2 mouse =
                Raylib.GetMousePosition();

            // -----------------------
            // TYPE FIELD
            // -----------------------

            if(Raylib.CheckCollisionPointRec(
                mouse,
                _layout.TypeField))
            {
                _typeDropdownOpen =
                    !_typeDropdownOpen;
                
                _editingName = false;

                return InspectorResult.None;
            }

            // -----------------------
            // TYPE DROPDOWN OPTIONS
            // -----------------------


            if(_typeDropdownOpen)
            {
                Rectangle genericOption =
                    _layout.TypeGenericOption;

                Rectangle playerOption =
                    _layout.TypePlayerOption;


                if(Raylib.CheckCollisionPointRec(
                    mouse,
                    genericOption))
                {
                    if(Target.Type != EntityType.Generic)
                    {
                        Target.Type =
                            EntityType.Generic;

                        _typeDropdownOpen = false;

                        return InspectorResult.Changed;
                    }

                    _typeDropdownOpen = false;

                    return InspectorResult.Changed;                
            
                }


                if(Raylib.CheckCollisionPointRec(
                    mouse,
                    playerOption))
                {
                    if(Target.Type != EntityType.Player)
                    {
                        Target.Type =
                            EntityType.Player;

                        _typeDropdownOpen = false;

                        return InspectorResult.Changed;
                    }

                    _typeDropdownOpen = false;

                    return InspectorResult.None;
                }
                
                // Click outside dropdown
                _typeDropdownOpen = false;
            }

            // -----------------------
            // NAME FIELD
            // -----------------------

            if(Raylib.CheckCollisionPointRec(
                mouse,
                _layout.NameField))
            {
                _editingName = true;

                return InspectorResult.None;
            }
            
            _editingName = false;
            
        }

        // -----------------------
        // NAME EDITING
        // -----------------------

        if(!_editingName)
            return InspectorResult.None;

        if(Raylib.IsKeyPressed(
            KeyboardKey.Escape))
        {
            _nameBuffer =
                Target.Name;        

            _editingName = false;       

            return InspectorResult.None;
        }

        if(Raylib.IsKeyPressed(
            KeyboardKey.Enter))
        {
            if(Target.Name != _nameBuffer)
            {
                Target.Name =
                    _nameBuffer;

                _editingName = false;

                return InspectorResult.Changed;
            }
            
            _editingName = false;

            return InspectorResult.None;            
            
        }

        if(Raylib.IsKeyPressed(
            KeyboardKey.Backspace))
        {
            if(_nameBuffer.Length > 0)
            {
                _nameBuffer =
                    _nameBuffer[..^1];
            }

            return InspectorResult.None;
        }

        int character =
            Raylib.GetCharPressed();

        if(character == 0)
            return InspectorResult.None;

        if(character >= 32)
        {
            _nameBuffer +=
                char.ConvertFromUtf32(character);
        }

        return InspectorResult.None;
    }

    public void Draw()
    {
        if(!Visible || Target == null)
            return;

        Raylib.DrawRectangle(
            InspectorLayout.X,
            InspectorLayout.Y,
            InspectorLayout.Width,
            680,
            Color.DarkGray
        );

        Raylib.DrawRectangleLines(
            InspectorLayout.X,
            InspectorLayout.Y,
            InspectorLayout.Width,
            680,
            Color.White
        );


        Raylib.DrawText(
            "ENTITY",
            1040,
            60,
            22,
            Color.White
        );


        Raylib.DrawText(
            "Name:",
            _layout.LabelX,
            InspectorLayout.Y + 60,
            18,
            Color.White
        );

        Raylib.DrawRectangleRec(
            _layout.NameField,
            Color.Gray
        );

        Raylib.DrawText(
            _nameBuffer,
            (int)_layout.NameField.X + 8,
            (int)_layout.NameField.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Type:",
            _layout.LabelX,
            InspectorLayout.Y + 100,
            18,
            Color.White
        );

        Raylib.DrawRectangleRec(
            _layout.TypeField,
            Color.Gray
        );

        Raylib.DrawText(
            Target.Type.ToString(),
            (int)_layout.TypeField.X + 8,
            (int)_layout.TypeField.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "▼",
            (int)_layout.TypeField.X + 138,
            (int)_layout.TypeField.Y + 6,
            18,
            Color.White
        );


        Raylib.DrawText(
            $"Position X: {Target.Position.X}",
            1040,
            180,
            18,
            Color.White
        );


        Raylib.DrawText(
            $"Position Y: {Target.Position.Y}",
            1040,
            210,
            18,
            Color.White
        );


        Raylib.DrawText(
            $"Sprite: {Target.Sprite.AssetName}",
            1040,
            240,
            18,
            Color.White
        );

        if(_typeDropdownOpen)
        {
            DrawTypeDropdown();
        }
    }
}