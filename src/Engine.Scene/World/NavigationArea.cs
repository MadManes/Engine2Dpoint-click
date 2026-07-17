using System.Collections.Generic;
using Engine.Math;

namespace Engine.Scene.World;

public class NavigationArea
{
    private readonly List<IWalkableArea> _areas = new();


    public IReadOnlyList<IWalkableArea> Areas => _areas;


    public void AddArea(IWalkableArea area)
    {
        _areas.Add(area);
    }
    
    public bool Contains(Vector2 position)
    {
        foreach (IWalkableArea area in _areas)
        {
            if (area.Contains(position))
                return true;
        }        

        return false;
    }

    public Vector2 GetClosestPoint(Vector2 position)
    {
        if (_areas.Count == 0)
            throw new InvalidOperationException(
                "NavigationArea does not contain any walkable areas.");
    
        Vector2 closest = _areas[0].GetClosestPoint(position);
    
        float minimumDistance = position.Distance(closest);
    
        foreach (IWalkableArea area in _areas)
        {
            Vector2 candidate = area.GetClosestPoint(position);
    
            float distance = position.Distance(candidate);
    
            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                closest = candidate;
            }
        }
    
        return closest;
    }
}