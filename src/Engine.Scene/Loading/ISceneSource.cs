using Engine.Scene.Definitions;

namespace Engine.Scene.Loading;

public interface ISceneSource
{
    SceneData Load(string name);

    IReadOnlyCollection<string> Names { get; }
}