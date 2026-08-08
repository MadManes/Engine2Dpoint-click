namespace Engine.Editor.UI.Layout;

public class EditorLayout
{
    public int ToolbarHeight => 40;
    public int ToolbarX => 0;
    public int ToolbarY => 0;
    public int ToolbarWidth { get; private set; }

    public int SceneX => 0;
    public int SceneY => ToolbarHeight;
    public int SceneWidth { get; private set; }
    public int SceneHeight { get; private set; }


    public void Update(
        int windowWidth,
        int windowHeight)
    {
        ToolbarWidth = windowWidth;

        SceneWidth = windowWidth;
        SceneHeight = windowHeight - ToolbarHeight;
    }
}