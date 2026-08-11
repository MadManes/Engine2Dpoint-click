using Engine.Editor.Input;
using Engine.Editor.State;
using Engine.Editor.Viewport;
using Engine.Editor.Loading;

namespace Engine.Editor.Context;
public class EditorContext
{
    public EditorState State { get; }
    public EditorInputController Input { get; }
    public EditorViewport Viewport { get; }
    public EditorAssets? Assets { get; }


    public EditorContext(
        EditorState state,
        EditorInputController input,
        EditorViewport viewport,
        EditorAssets? assets)
    {
        State = state;
        Input = input;
        Viewport = viewport;
        Assets = assets;
    }
}