using Engine.Editor.Context;

namespace Engine.Editor.Tools;

public class ToolManager
{
    private readonly Dictionary<string, IEditorTool> _tools;


    public IEditorTool Current { get; private set; }


    public ToolManager()
    {
        _tools = new();

        Current = null!;
    }


    public void Register(
        string name,
        IEditorTool tool)
    {
        _tools.Add(
            name,
            tool
        );
    }


    public void Activate(
        string name)
    {
        if(_tools.TryGetValue(
            name,
            out IEditorTool? tool))
        {
            Current = tool;
        }
    }


    public void Update(
        EditorContext context)
    {
        Current?.Update(
            context
        );
    }
}