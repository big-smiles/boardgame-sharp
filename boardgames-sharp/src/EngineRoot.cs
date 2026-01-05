using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.GameState;
using boardgames_sharp.Interaction;
using boardgames_sharp.Player;
using boardgames_sharp.Stack;

namespace boardgames_sharp;

public class EngineRoot
{
    internal PlayerManager PlayerManager { get; }
    internal GameStateObservable GameStateObservable { get; } = new();
    internal GameStateManager GameStateManager { get; } = new();
    internal ActionPerformer ActionPerformer { get; } = new();
    internal ActionStack ActionStack { get; } = new();
    private readonly EntityManager _entityManager = new EntityManager();
    public IEntityManager EntityManager => _entityManager;
    
    private readonly InteractionManager _interactionManager = new();
    public IInteractionManager InteractionManager => _interactionManager;

    public EngineRoot(HashSet<uint> players)
    {
        PlayerManager = new PlayerManager(players);
        _initialize();
    }

    private void _initialize()
    {
        GameStateObservable.initialize(this);
        ActionPerformer.initialize(this);
        ActionStack.initialize(this);
        GameStateManager.initialize(this);
        _entityManager.initialize(this);
        _interactionManager.initialize(this);
    }
}

public interface IInitializeWithEngineRoot
{
    void  initialize(EngineRoot engineRoot);
}

public class BadRootInitializationException(string message) : Exception(message)
{
    
}