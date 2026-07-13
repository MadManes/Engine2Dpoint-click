namespace Engine.Core;

public interface IGameLoop
{
    void Initialize();
    void Update(float deltaTime);
    void Render();
    void Shutdown();
}