using Engine.Editor.Input;
using Engine.Editor.State;

namespace Engine.Editor.Context;
public class EditorContext
{
    public EditorState State { get; }
    public EditorInputController Input { get; }


    public EditorContext(
        EditorState state,
        EditorInputController input)
    {
        State = state;
        Input = input;
    }
}