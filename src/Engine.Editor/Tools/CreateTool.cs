using System;
using Engine.Math;
using Engine.Editor.Context;
using Engine.Scene.Definitions;
using Engine.Entities.Components;
using Raylib_cs;
using Engine.Editor.Creation;

namespace Engine.Editor.Tools;

public class CreateTool : IEditorTool
{
    private readonly EditorEntityFactory _factory;

    public CreateTool(
        EditorEntityFactory factory)
    {
        _factory = factory;
    }


    public void Update(
        EditorContext context)
    {
        System.Numerics.Vector2 mouse =
            Raylib.GetMousePosition();


        if(!context.Viewport.Contains(mouse))
            return;


        if(!context.Input.LeftPressed())
            return;


        Vector2 position =
            context.Input.MouseWorldPosition();


        EntityData entity =
            _factory.CreateGeneric();

        entity.Position =
            context.Input.MouseWorldPosition();


        context.State.CurrentDocument!
            .Scene
            .EntityCollection
            .Entities
            .Add(entity);


        context.State.SelectEntity(
            entity
        );

        context.State.MarkCurrentDocumentDirty();
        
    }
}