using Engine.Core;
using Engine.Game.State;
using Engine.Scene;
using Engine.Scene.Scenes;
using Engine.Runtime.Assets;
using Engine.Runtime.Systems;
using Engine.Runtime.Controllers;
using Engine.Runtime.Rendering;
using Engine.Runtime.Debug;
using Engine.Navigation;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Math;
using Raylib_cs;
using Engine.Entities.Sprites;
using Engine.Runtime.Interaction;
using Engine.Runtime.SceneTransition;
//using Engine.Runtime.Systems;

namespace Engine.Runtime;

public class Game : IGameLoop
{
    private readonly IRenderer _renderer;
    private readonly MovementSystem _movementSystem;
    private readonly NavigationController _navigationController;
    private readonly DebugController _debugController;
    private readonly DebugSettings _debugSettings;
    private readonly SceneDebugController _sceneDebugController;
    private readonly WorldStateApplier _worldStateApplier;
    private readonly SceneManager _sceneManager;
    private readonly AssetManager _assetManager;
    private readonly EntityPicker _entityPicker;
    private readonly InteractionSystem _interactionSystem;
    private readonly InteractionController _interactionController;
    private readonly PlayerController _playerController;
    private readonly GameState _gameState;    
    private readonly Camera _camera;
    private Vector2? _mousePosition;
    private Vector2? _correctedPosition;
    
    public Game()
    {
        _debugSettings = new DebugSettings();

        _camera = new Camera();
        _camera.SetZoom(1.0f);

        _assetManager = new AssetManager(
            "assets"
        );

        _entityPicker = new EntityPicker(
            _assetManager
        );        
        
        _debugController = new DebugController(_debugSettings, _camera); 
        _renderer = new DebugRenderer(
            _debugSettings,
            _camera,
            _assetManager
        );

        _movementSystem = new MovementSystem();
        _navigationController = new NavigationController(
            new DirectPathFinder()
        );

        
        
        _worldStateApplier = new WorldStateApplier();

        Entity player = new(
            "Player",
            "Guybrush",
            EntityType.Player,
            new SpriteComponent(
                new SpriteSet(
                    "guybrush_idle"
                ),
                new SpritePivot(
                    16,
                    32
                )
            )
        );        

        _gameState = new GameState(player);  

        _sceneManager = new SceneManager();

        _sceneManager.RegisterPersistentEntity(
            _gameState.Player
        );

        _sceneManager.LoadScene(
            SceneRegistry.Create("TestRoom")
        );

        DefaultInteractionHandler defaultHandler =
            new();

        SceneTransitionService transitionService =
            new(_sceneManager);

        DoorInteractionHandler doorHandler =
            new(transitionService);

        InteractionHandlerResolver resolver =
            new(
                defaultHandler,
                doorHandler
            );

        InteractionExecutor executor =
            new(resolver);

        _interactionSystem =
            new InteractionSystem(executor);

        

        _playerController = new PlayerController(_gameState.Player);

        _interactionController = new InteractionController(
            _navigationController,
            _interactionSystem,
            _playerController
        );

        
        

        // PRUEBA
        var state =
            _gameState.WorldState
                .GetSceneState("Test Room")
                .GetEntityState("object_2");

        //state.SetState("Hidden");

        //var state =
        //    _gameState.WorldState
        //        .GetSceneState("Test Room")
        //        .GetEntityState("object_2");
        //
        //state.SetState("TestState");
        
        _worldStateApplier.Apply(
            _gameState,
            _sceneManager.CurrentScene!
        );

        _sceneDebugController = new SceneDebugController(
            _sceneManager,            
            _sceneManager.CurrentScene!.Name
        );

        // PRUEBA
        _sceneDebugController.SceneChanged += OnSceneChanged;
    }

    // PRUEBA
    private void OnSceneChanged(GameScene scene)
    {
        if (scene.Name == "Wide Room")
        {
            SceneState testRoomState =
                _gameState.WorldState.GetSceneState(
                    "Test Room"
                );

            testRoomState
                .GetEntityState("object_1")
                .Remove();
        }

        _worldStateApplier.Apply(
            _gameState,
            scene
        );
    }

    public void Run()
    {
        Initialize();

        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Time.DeltaTime();

            Update(deltaTime);
            Render();
        }

        Shutdown();
    }

    public void Initialize()
    {
        _renderer.Initialize();
    }

    public void Update(float deltaTime)    
    {
        _debugController.Update();
        _sceneDebugController.Update();

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            System.Numerics.Vector2 mouse = Raylib.GetMousePosition();

            Vector2 clickPosition = _camera.ScreenToWorld(
                mouse.X,
                mouse.Y
            );

            _mousePosition = clickPosition;

            Entity? selectedEntity =
                _entityPicker.Pick(
                    _sceneManager.CurrentScene!.World.Entities,
                    clickPosition
                );
            
            // Por ahora
            if (selectedEntity != null)
            {   
                _interactionController.HandleEntityClick(
                    selectedEntity,
                    _gameState.Player,
                    _sceneManager.CurrentScene!.World
                );

                return;
            }
        
            NavigationPath path = _navigationController.GetPath(
                _sceneManager.CurrentScene!.World,
                _gameState.Player.Transform.Position,
                clickPosition
            );

    
            if (!path.IsEmpty)
            {
                Vector2 destination = path.Points[0];
                _correctedPosition = destination;                
                _playerController.MoveTo(path);
            }
        }

        _movementSystem.Update(_sceneManager.CurrentScene!.World, deltaTime);
        _interactionSystem.Update(_gameState.Player);
        
    }

    public void Render()
    {
        RenderContext context = new(
            _sceneManager.CurrentScene!,
            _sceneManager.CurrentScene!.Name,
            _mousePosition,
            _correctedPosition
        );
        _renderer.Render(context);
    }

    public void Shutdown()
    {
        _assetManager.Dispose();
        _renderer.Shutdown();
    }
}