using Raylib_cs;
using Engine.Entities.Components;
using Engine.Scene.Definitions;
using Engine.Editor.Loading;

namespace Engine.Editor.UI;

public class EditorInspector
{
    private const int Width = 260;

    private bool _editingName;

    private bool _typeDropdownOpen;
    private bool _spriteDropdownOpen;
    private bool _interactableDropdownOpen;
    private bool _doorDropdownOpen;
    private bool _targetSceneDropdownOpen;
    private bool _targetSpawnDropdownOpen;

    private EditorAssets? _assets;
    private SceneProject? _project;

    private string _nameBuffer = "";

    public bool Visible { get; private set; }

    public EntityData? Target { get; private set; }

    private readonly InspectorLayout _layout;


    public bool IsEditing =>
        _editingName ||
        _typeDropdownOpen ||
        _spriteDropdownOpen ||
        _interactableDropdownOpen ||
        _doorDropdownOpen ||
        _targetSceneDropdownOpen ||
        _targetSpawnDropdownOpen;


    public EditorInspector()
    {
        _layout = new InspectorLayout();
    }

    public void UpdateLayout(int windowWidth)
    {
        _layout.Update(windowWidth);
    }
    
    public void SetAssets(
        EditorAssets assets)
    {
        _assets = assets;
    }

    public void SetProject(
        SceneProject project)
    {
        _project = project;
    }

    public void Open(
        EntityData entity)
    {
        Target = entity;
        _nameBuffer = entity.Name;

        _editingName = false;
        _typeDropdownOpen = false;
        _spriteDropdownOpen = false;
        _interactableDropdownOpen = false;
        _doorDropdownOpen = false;
        _targetSceneDropdownOpen = false;
        _targetSpawnDropdownOpen = false;

        Visible = true;
    }


    public void Close()
    {
        Target = null;
        Visible = false;
    }

    private bool IsDoorConfigurationValid()
    {
        if(Target?.Door == null)
            return true;

        if(string.IsNullOrWhiteSpace(
            Target.Door.TargetScene))
        {
            return false;
        }

        if(string.IsNullOrWhiteSpace(
            Target.Door.TargetSpawn))
        {
            return false;
        }

        return true;
    }

    private void DrawSpriteDropdown()
    {
        if(_assets == null)
            return;

        int index = 0;

        foreach(string assetName in _assets.SpriteNames)
        {
            Rectangle option =
                _layout.GetSpriteOption(index);

            Raylib.DrawRectangle(
                (int)option.X,
                (int)option.Y,
                (int)option.Width,
                (int)option.Height,
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
                assetName,
                (int)option.X + 8,
                (int)option.Y + 6,
                18,
                Color.White
            );

            index++;
        }
    }

    private void DrawInteractableDropdown()
    {
        Rectangle yes =
            _layout.InteractableYesOption;

        Rectangle no =
            _layout.InteractableNoOption;

        int bottom =
            (int)no.Y +
            (int)no.Height;

        Raylib.DrawRectangle(
            (int)yes.X,
            (int)yes.Y,
            (int)yes.Width,
            bottom - (int)yes.Y,
            Color.DarkGray
        );

        Raylib.DrawRectangleLines(
            (int)yes.X,
            (int)yes.Y,
            (int)yes.Width,
            bottom - (int)yes.Y,
            Color.White
        );

        Raylib.DrawText(
            "Yes",
            (int)yes.X + 8,
            (int)yes.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "No",
            (int)no.X + 8,
            (int)no.Y + 6,
            18,
            Color.White
        );
    }

    private void DrawDoorDropdown()
    {
        Rectangle yes =
            _layout.DoorYesOption;

        Rectangle no =
            _layout.DoorNoOption;

        int bottom =
            (int)no.Y +
            (int)no.Height;

        Raylib.DrawRectangle(
            (int)yes.X,
            (int)yes.Y,
            (int)yes.Width,
            bottom - (int)yes.Y,
            Color.DarkGray
        );

        Raylib.DrawRectangleLines(
            (int)yes.X,
            (int)yes.Y,
            (int)yes.Width,
            bottom - (int)yes.Y,
            Color.White
        );

        Raylib.DrawText(
            "Yes",
            (int)yes.X + 8,
            (int)yes.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "No",
            (int)no.X + 8,
            (int)no.Y + 6,
            18,
            Color.White
        );
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

    private void DrawTargetSceneDropdown()
    {
        if(_project == null)
            return;

        int index = 0;

        foreach(string sceneId in _project.SceneIds)
        {
            Rectangle option =
                _layout.GetTargetSceneOption(index);

            Raylib.DrawRectangle(
                (int)option.X,
                (int)option.Y,
                (int)option.Width,
                (int)option.Height,
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

    private void DrawTargetSpawnDropdown()
    {
        if(_project == null ||
           Target?.Door == null)
        {
            return;
        }

        if(string.IsNullOrWhiteSpace(
            Target.Door.TargetScene))
        {
            return;
        }

        SceneData scene =
            _project.GetScene(
                Target.Door.TargetScene
            );

        int index = 0;

        foreach(SpawnPointData spawn in scene.SpawnPoints)
        {
            Rectangle option =
                _layout.GetTargetSpawnOption(index);

            Raylib.DrawRectangle(
                (int)option.X,
                (int)option.Y,
                (int)option.Width,
                (int)option.Height,
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
                spawn.Id,
                (int)option.X + 8,
                (int)option.Y + 6,
                18,
                Color.White
            );

            index++;
        }
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

            if(_targetSceneDropdownOpen && _project != null)
            {
                int index = 0;

                foreach(string sceneId in _project.SceneIds)
                {
                    Rectangle option =
                        _layout.GetTargetSceneOption(index);

                    if(Raylib.CheckCollisionPointRec(
                        mouse,
                        option))
                    {
                        if(Target.Door == null)
                        {
                            Target.Door =
                                new DoorData();
                        }

                        if(Target.Door.TargetScene != sceneId)
                        {
                            Target.Door.TargetScene =
                                sceneId;

                            Target.Door.TargetSpawn = "";

                            _targetSceneDropdownOpen = false;

                            return InspectorResult.Changed;
                        }

                        _targetSceneDropdownOpen = false;

                        return InspectorResult.None;
                    }

                    index++;
                }

                _targetSceneDropdownOpen = false;
            }


            // -----------------------
            // DOOR DROPDOWN
            // -----------------------

            if(_doorDropdownOpen)
            {
                Rectangle yesOption =
                    _layout.DoorYesOption;

                Rectangle noOption =
                    _layout.DoorNoOption;


                if(Raylib.CheckCollisionPointRec(
                    mouse,
                    yesOption))
                {
                    if(Target.Door == null)
                    {
                        Target.Door =
                            new DoorData();

                        _doorDropdownOpen = false;

                        return InspectorResult.Changed;
                    }

                    _doorDropdownOpen = false;

                    return InspectorResult.None;
                }


                if(Raylib.CheckCollisionPointRec(
                    mouse,
                    noOption))
                {
                    if(Target.Door != null)
                    {
                        Target.Door = null;

                        _doorDropdownOpen = false;

                        return InspectorResult.Changed;
                    }

                    _doorDropdownOpen = false;

                    return InspectorResult.None;
                }


                // Click fuera del dropdown.
                _doorDropdownOpen = false;

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
            // INTERACTABLE DROPDOWN
            // -----------------------

            if(_interactableDropdownOpen)
            {
                Rectangle yesOption =
                    _layout.InteractableYesOption;

                Rectangle noOption =
                    _layout.InteractableNoOption;


                if(Raylib.CheckCollisionPointRec(
                    mouse,
                    yesOption))
                {
                    if(!Target.IsInteractable)
                    {
                        Target.IsInteractable = true;

                        _interactableDropdownOpen = false;

                        return InspectorResult.Changed;
                    }

                    _interactableDropdownOpen = false;

                    return InspectorResult.None;
                }


                if(Raylib.CheckCollisionPointRec(
                    mouse,
                    noOption))
                {
                    if(Target.IsInteractable)
                    {
                        Target.IsInteractable = false;

                        _interactableDropdownOpen = false;

                        return InspectorResult.Changed;
                    }

                    _interactableDropdownOpen = false;

                    return InspectorResult.None;
                }


                // Click fuera del dropdown.
                _interactableDropdownOpen = false;

                return InspectorResult.None;
            }            
            


            // -----------------------
            // SPRITE DROPDOWN
            // -----------------------

            if(_spriteDropdownOpen)
            {
                if(_assets != null)
                {
                    int index = 0;

                    foreach(string assetName in _assets.SpriteNames)
                    {
                        Rectangle option =
                            _layout.GetSpriteOption(index);

                        if(Raylib.CheckCollisionPointRec(
                            mouse,
                            option))
                        {
                            if(Target.Sprite.AssetName != assetName)
                            {
                                Target.Sprite.AssetName =
                                    assetName;

                                _spriteDropdownOpen = false;

                                return InspectorResult.Changed;
                            }

                            _spriteDropdownOpen = false;

                            return InspectorResult.None;
                        }

                        index++;
                    }
                }

                // Click fuera del dropdown.
                _spriteDropdownOpen = false;

                return InspectorResult.None;
            }

            if(_targetSpawnDropdownOpen &&
               _project != null &&
               Target.Door != null &&
               !string.IsNullOrWhiteSpace(
                   Target.Door.TargetScene))
            {
                SceneData scene =
                    _project.GetScene(
                        Target.Door.TargetScene
                    );

                int index = 0;

                foreach(SpawnPointData spawn in scene.SpawnPoints)
                {
                    Rectangle option =
                        _layout.GetTargetSpawnOption(index);

                    if(Raylib.CheckCollisionPointRec(
                        mouse,
                        option))
                    {
                        if(Target.Door.TargetSpawn != spawn.Id)
                        {
                            Target.Door.TargetSpawn =
                                spawn.Id;

                            _targetSpawnDropdownOpen = false;

                            return InspectorResult.Changed;
                        }

                        _targetSpawnDropdownOpen = false;

                        return InspectorResult.None;
                    }

                    index++;
                }

                _targetSpawnDropdownOpen = false;
            }

            if(Raylib.CheckCollisionPointRec(
                mouse,
                _layout.TargetSpawnField))
            {
                if(Target.Door == null ||
                   string.IsNullOrWhiteSpace(
                       Target.Door.TargetScene))
                {
                    return InspectorResult.None;
                }

                _targetSpawnDropdownOpen =
                    !_targetSpawnDropdownOpen;

                _editingName = false;
                _typeDropdownOpen = false;
                _spriteDropdownOpen = false;
                _interactableDropdownOpen = false;
                _doorDropdownOpen = false;

                return InspectorResult.None;
            }

            // CAMPOS
            if(Raylib.CheckCollisionPointRec(
                mouse,
                _layout.TargetSceneField))
            {
                if(Target.Door == null)
                {
                    Target.Door =
                        new DoorData();
                }

                _targetSceneDropdownOpen =
                    !_targetSceneDropdownOpen;

                _editingName = false;
                _typeDropdownOpen = false;
                _spriteDropdownOpen = false;
                _interactableDropdownOpen = false;
                _doorDropdownOpen = false;

                return InspectorResult.None;
            }

            // -----------------------
            // DOOR FIELD
            // -----------------------

            if(Raylib.CheckCollisionPointRec(
                mouse,
                _layout.DoorField))
            {
                _doorDropdownOpen =
                    !_doorDropdownOpen;

                _editingName = false;
                _typeDropdownOpen = false;
                _spriteDropdownOpen = false;
                _interactableDropdownOpen = false;

                return InspectorResult.None;
            }



            // -----------------------
            // INTERACTABLE FIELD
            // -----------------------

            if(Raylib.CheckCollisionPointRec(
                mouse,
                _layout.InteractableField))
            {
                _interactableDropdownOpen =
                    !_interactableDropdownOpen;

                _editingName = false;
                _typeDropdownOpen = false;
                _spriteDropdownOpen = false;

                return InspectorResult.None;
            }

            //// Y ESTO?
            /// // -----------------------
            // INTERACTABLE FIELD
            // -----------------------

            //if(Raylib.CheckCollisionPointRec(
            //    mouse,
            //    _layout.InteractableField))
            //{
            //    Target.IsInteractable =
            //        !Target.IsInteractable;

            //    return InspectorResult.Changed;
            //}

            // -----------------------
            // SPRITE FIELD
            // -----------------------

            if(Raylib.CheckCollisionPointRec(
                mouse,
                _layout.SpriteField))
            {
                _spriteDropdownOpen =
                    !_spriteDropdownOpen;

                _editingName = false;
                _typeDropdownOpen = false;

                return InspectorResult.None;
            }



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
            _layout.X,
            InspectorLayout.Y,
            InspectorLayout.Width,
            680,
            Color.DarkGray
        );

        Raylib.DrawRectangleLines(
            _layout.X,
            InspectorLayout.Y,
            InspectorLayout.Width,
            680,
            Color.White
        );


        Raylib.DrawText(
            "ENTITY",
            _layout.LabelX,
            InspectorLayout.Y + 20,
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
            250,
            18,
            Color.White
        );


        Raylib.DrawText(
            $"Position Y: {Target.Position.Y}",
            1040,
            310,
            18,
            Color.White
        );


        Raylib.DrawText(
            "Sprite:",
            _layout.LabelX,
            InspectorLayout.Y + 140,
            18,
            Color.White
        );

        Raylib.DrawRectangleRec(
            _layout.SpriteField,
            Color.Gray
        );

        Raylib.DrawText(
            Target.Sprite.AssetName,
            (int)_layout.SpriteField.X + 8,
            (int)_layout.SpriteField.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "▼",
            (int)_layout.SpriteField.X + 138,
            (int)_layout.SpriteField.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Interactable:",
            _layout.LabelX,
            InspectorLayout.Y + 180,
            18,
            Color.White
        );

        Raylib.DrawRectangleRec(
            _layout.InteractableField,
            Color.Gray
        );

        Raylib.DrawText(
            Target.IsInteractable ? "Yes" : "No",
            (int)_layout.InteractableField.X + 8,
            (int)_layout.InteractableField.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "▼",
            (int)_layout.InteractableField.X + 138,
            (int)_layout.InteractableField.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Door:",
            _layout.LabelX,
            (int)_layout.DoorField.Y + 5,
            18,
            Color.White
        );

        Raylib.DrawRectangleRec(
            _layout.DoorField,
            Color.Gray
        );

        Raylib.DrawText(
            Target.Door != null ? "Yes" : "No",
            (int)_layout.DoorField.X + 8,
            (int)_layout.DoorField.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "▼",
            (int)_layout.DoorField.X + 138,
            (int)_layout.DoorField.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Target Scene:",
            _layout.LabelX,
            (int)_layout.TargetSceneField.Y + 5,
            18,
            Color.White
        );

        Raylib.DrawRectangleRec(
            _layout.TargetSceneField,
            Color.Gray
        );

        Raylib.DrawText(
            Target.Door?.TargetScene ?? "",
            (int)_layout.TargetSceneField.X + 8,
            (int)_layout.TargetSceneField.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "▼",
            (int)_layout.TargetSceneField.X + 138,
            (int)_layout.TargetSceneField.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "Target Spawn:",
            _layout.LabelX,
            (int)_layout.TargetSpawnField.Y + 5,
            18,
            Color.White
        );

        Raylib.DrawRectangleRec(
            _layout.TargetSpawnField,
            Color.Gray
        );

        Raylib.DrawText(
            Target.Door?.TargetSpawn ?? "",
            (int)_layout.TargetSpawnField.X + 8,
            (int)_layout.TargetSpawnField.Y + 6,
            18,
            Color.White
        );

        Raylib.DrawText(
            "▼",
            (int)_layout.TargetSpawnField.X + 138,
            (int)_layout.TargetSpawnField.Y + 6,
            18,
            Color.White
        );

        if(Target.Door != null)
        {
            string status =
                IsDoorConfigurationValid()
                    ? "Door configuration: OK"
                    : "Door configuration: INCOMPLETE";
        
            Raylib.DrawText(
                status,
                _layout.LabelX,
                (int)_layout.TargetSpawnField.Y + 45,
                16,
                IsDoorConfigurationValid()
                    ? Color.Green
                    : Color.Red
            );
        }

        if(_typeDropdownOpen)
        {
            DrawTypeDropdown();
        }

        if(_spriteDropdownOpen)
        {
            DrawSpriteDropdown();
        }

        if(_interactableDropdownOpen)
        {
            DrawInteractableDropdown();
        }

        if(_doorDropdownOpen)
        {
            DrawDoorDropdown();
        }

        if(_targetSceneDropdownOpen)
        {
            DrawTargetSceneDropdown();
        }

        if(_targetSpawnDropdownOpen)
        {
            DrawTargetSpawnDropdown();
        }
    }
}