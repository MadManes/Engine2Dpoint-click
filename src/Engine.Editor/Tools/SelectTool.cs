using Engine.Math;
using Engine.Editor.Context;
using Engine.Editor.Selection;
using Engine.Scene.Definitions;
using Raylib_cs;

namespace Engine.Editor.Tools;

public class SelectTool : IEditorTool
{
    private readonly EditorEntityPicker _picker;


    public SelectTool(
        EditorEntityPicker picker)
    {
        _picker = picker;
    }


    public void Update(
        EditorContext context)
    {
        if(!context.Input.LeftPressed())
            return;


        System.Numerics.Vector2 mouse =
            Raylib.GetMousePosition();


        if(!context.Viewport.Contains(mouse))
            return;


        Vector2 mouseWorld =
            context.Input.MouseWorldPosition();


        EntityData? selected =
            _picker.Pick(
                context.State.CurrentDocument!
                    .Scene
                    .EntityCollection
                    .Entities,
                mouseWorld
            );


        context.State.SelectEntity(
            selected
        );
    }
}