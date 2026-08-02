using Engine.Math;
using Engine.Core;

namespace Engine.Entities.Components;

public class Movement : Component
{
    public NavigationPath? Path { get; private set; }

    public bool HasFinishedPath {get; private set; } = false;

    public float Speed { get; set; }

    public Movement()
    {
        Path = null;
        Speed = 150f;
    }

    public Vector2 CurrentTarget => Path!.CurrentPoint;

    public void SetPath(NavigationPath path)
    {
        Path = path;
        HasFinishedPath = false;
    }    

    public bool AdvancePath()
    {
        if (Path == null)
        {
            return false;
        }        

        return Path.MoveNext();
    }

    public bool IsMoving()
    {
        return Path != null; 
    }

    public void CompletePath()
    {
        Path = null;
        HasFinishedPath = true;
    }

    public void ResetFinishedPath()
    {
        HasFinishedPath = false;
    }

    public void ClearPath()
    {
        Path = null;
    }
}