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

        DrawSpawnPoints(
            document
        );

        DrawEntities(
            document
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


    private void DrawSpawnPoints(
        SceneDocument document)
    {
        foreach(var spawn in document.Scene.SpawnPoints)
        {
            Raylib.DrawCircle(
                (int)spawn.Position.X,
                (int)spawn.Position.Y,
                6,
                Color.Blue
            );


            Raylib.DrawText(
                spawn.Id,
                (int)spawn.Position.X + 8,
                (int)spawn.Position.Y,
                12,
                Color.White
            );
        }
    }


    private void DrawEntities(
        SceneDocument document)
    {
        foreach(var entity in document.Scene.EntityCollection.Entities)
        {
            Raylib.DrawCircle(
                (int)entity.Position.X,
                (int)entity.Position.Y,
                8,
                Color.Red
            );


            Raylib.DrawText(
                entity.Id,
                (int)entity.Position.X + 10,
                (int)entity.Position.Y,
                12,
                Color.White
            );
        }
    }
}