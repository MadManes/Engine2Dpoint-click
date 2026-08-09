using Raylib_cs;
using Engine.Math;
using Engine.Editor.Documents;
using Engine.Editor.Loading;
using Engine.Entities.Rendering;
using Engine.Scene.Definitions;

namespace Engine.Editor.Rendering;

public class SceneViewRenderer
{
    private readonly EditorSpritePlacement _placement;

    public SceneViewRenderer()
    {
        _placement = new EditorSpritePlacement();
    }

    public void Render(
        SceneDocument document,
        EditorAssets assets,
        EntityData? selectedEntity)
    {
        Raylib.DrawTexture(
            assets.Background,
            0,
            0,
            Color.White
        );

        DrawNavigation(
            document
        );        

        DrawEntities(
            document,
            assets
        );

        DrawSelection(
            selectedEntity,
            assets
        );
    }


    private void DrawNavigation(
        SceneDocument document)
    {
        var vertices =
            document.Scene.Navigation.Vertices;


        for(int i = 0; i < vertices.Count; i++)
        {
            Vector2 current =
                vertices[i];

            Vector2 next =
                vertices[
                    (i + 1) % vertices.Count
                ];


            Raylib.DrawLine(
                (int)current.X,
                (int)current.Y,
                (int)next.X,
                (int)next.Y,
                Color.Green
            );
        }
    }
    
    private void DrawEntities(
    SceneDocument document,
    EditorAssets assets)
    {
        foreach(var entity in document.Scene.EntityCollection.Entities)
        {
            if (string.IsNullOrWhiteSpace(entity.Sprite.AssetName))
            {
                Raylib.DrawCircle(
                    (int)entity.Position.X,
                    (int)entity.Position.Y,
                    8,
                    Color.Magenta
                );

                continue;
            }
            Texture2D texture =
                assets.GetSprite(
                    entity.Sprite.AssetName
                );

            Vector2 drawPosition = _placement.GetDrawPosition(entity);
            
            Raylib.DrawTexture(
                texture,
                (int)drawPosition.X,
                (int)drawPosition.Y,
                Color.White
            );
        }
            
    }

    private void DrawSelection(
        EntityData? entity,
        EditorAssets assets)
    {
        if(entity == null)
            return; 


        if(string.IsNullOrWhiteSpace(
            entity.Sprite.AssetName))
        {
            Raylib.DrawCircleLines(
                (int)entity.Position.X,
                (int)entity.Position.Y,
                12,
                Color.Yellow
            );  

            return;
        }   


        Texture2D texture =
            assets.GetSprite(
                entity.Sprite.AssetName
            );  


        Vector2 drawPosition =
            _placement.GetDrawPosition(
                entity
            );  


        Raylib.DrawRectangleLines(
            (int)drawPosition.X,
            (int)drawPosition.Y,
            texture.Width,
            texture.Height,
            Color.Yellow
        );
    }
    

}