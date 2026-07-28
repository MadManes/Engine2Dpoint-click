using Raylib_cs;

namespace Engine.Runtime.Rendering.Backgrounds;

public static class BackgroundCatalog
{
    public static Color GetColor(string assetName)
    {
        return assetName switch
        {
            "test_room" => Color.DarkBlue,

            "wide_room" => Color.DarkGreen,

            _ => Color.Black
        };
    }
}