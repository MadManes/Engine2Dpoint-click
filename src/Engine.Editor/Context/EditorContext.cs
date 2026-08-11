using Engine.Editor.Input;
using Engine.Editor.State;
using Engine.Editor.Viewport;

namespace Engine.Editor.Context;

public class EditorContext
{
    public EditorState State { get; }
    public EditorInputController Input { get; }
    public EditorViewport Viewport { get; }


    public EditorContext(
        EditorState state,
        EditorInputController input,
        EditorViewport viewport)
    {
        State = state;
        Input = input;
        Viewport = viewport;
    }
}