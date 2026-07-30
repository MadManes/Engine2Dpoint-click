namespace Engine.Game.State;

public class WorldState
{
    private readonly Dictionary<string, SceneState> _scenes = new();


    public SceneState GetSceneState(string sceneName)
    {
        if (!_scenes.ContainsKey(sceneName))
        {
            _scenes.Add(
                sceneName,
                new SceneState(sceneName)
            );
        }

        return _scenes[sceneName];
    }
}