using System;
using System.Collections.Generic;

namespace Engine.Entities.Components;

public class ComponentCollection
{
    private readonly Dictionary<Type, Component> _components = new();


    public void Add(Component component)
    {
        _components[component.GetType()] = component;
    }


    public bool Has<T>()
        where T : Component
    {
        return _components.ContainsKey(typeof(T));
    }


    public T? Get<T>()
        where T : Component
    {
        if (_components.TryGetValue(
            typeof(T),
            out Component? component))
        {
            return (T)component;
        }

        return null;
    }
}