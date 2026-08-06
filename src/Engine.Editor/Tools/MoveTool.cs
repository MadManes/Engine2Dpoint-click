using Engine.Math;
using Engine.Scene.Definitions;
using Engine.Editor.Context;
using Engine.Editor.Selection;

namespace Engine.Editor.Tools;

public class MoveTool : IEditorTool
{
    private Vector2 _dragOffset;
    private bool _dragging;

    private readonly EditorEntityPicker _picker;


    public MoveTool(
        EditorEntityPicker picker)
    {
        _picker = picker;
    }


    public void Update(
        EditorContext context)
    {
        Vector2 mouseWorld =
            context.Input.MouseWorldPosition();


        if(context.Input.LeftPressed())
        {
            EntityData? clicked =
                _picker.Pick(
                    context.State.CurrentDocument!
                        .Scene
                        .EntityCollection
                        .Entities,
                    mouseWorld
                );


            if(clicked == null)
                return;

            context.State.SelectEntity(
                clicked
            );            


            _dragOffset =
                new Vector2(
                    clicked.Position.X - mouseWorld.X,
                    clicked.Position.Y - mouseWorld.Y
                );


            _dragging = true;
        }


        if(context.Input.LeftDown()
            &&
            _dragging
            &&
            context.State.SelectedEntity != null)
        {
            EntityData entity =
                context.State.SelectedEntity;


            entity.Position =
                new Vector2(
                    mouseWorld.X + _dragOffset.X,
                    mouseWorld.Y + _dragOffset.Y
                );


            context.State.CurrentDocument!
                .MarkDirty();
        }


        if(context.Input.LeftReleased())
        {
            _dragging = false;
        }
    }
}