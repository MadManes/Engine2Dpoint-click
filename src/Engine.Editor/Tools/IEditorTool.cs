using Engine.Editor.Context;

namespace Engine.Editor.Tools;

public interface IEditorTool
{
    void Update(
        EditorContext context
    );
}