namespace Engine.Game.State;

public class SceneState
{
    private readonly Dictionary<string, EntityState> _entities = new();

    public string SceneName { get; }


    public SceneState(string sceneName)
    {
        SceneName = sceneName;
    }

    public EntityState GetEntityState(string entityId)
    {
        if (!_entities.ContainsKey(entityId))
        {
            _entities.Add(
                entityId,
                new EntityState(entityId)
            );
        }

        return _entities[entityId];
    }

}