//using Engine.Scene.Definitions;
//
//namespace Engine.Scene.Loading.Sources;
//
//public class DefinitionSceneSource : ISceneSource
//{
//    private readonly Dictionary<string, Func<SceneData>> _scenes =
//        new()
//        {
//            {
//                "TestRoom",
//                () => TestRoomDefinition.Create()
//            },
//            {
//                "WideRoom",
//                () => WideRoomDefinition.Create()
//            }
//        };
//
//    public IReadOnlyCollection<string> Names =>
//        _scenes.Keys;
//
//    public SceneData Load(string name)
//    {
//        if (!_scenes.TryGetValue(name, out var factory))
//        {
//            throw new InvalidOperationException(
//                $"Scene '{name}' not found."
//            );
//        }
//
//        return factory();
//    }
//}