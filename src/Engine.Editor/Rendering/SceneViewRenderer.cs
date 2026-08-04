using Engine.Editor.Documents;
using Engine.Editor.Loading;
using Engine.Math;
using Raylib_cs;

namespace Engine.Editor.Rendering;

public class SceneViewRenderer
{
    public void Render(
        SceneDocument document,
        EditorAssets assets)
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
            if (!string.IsNullOrWhiteSpace(entity.Sprite.AssetName))
                continue;
            
            Texture2D texture =
                assets.GetSprite(
                    entity.Sprite.AssetName
                );
            Raylib.DrawTexture(
                texture,
                (int)entity.Position.X,
                (int)entity.Position.Y,
                Color.White
            );
        }
            
    }
}