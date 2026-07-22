using Engine.Core;

namespace Engine.Navigation;

public interface IPathFinder
{
    NavigationPath FindPath(
        NavigationRequest request
    );
}