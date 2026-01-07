using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.GameState;
using boardgames_sharp.Interaction;
using boardgames_sharp.Phases;
using boardgames_sharp.Player;
using boardgames_sharp.Stack;

namespace boardgames_sharp;

internal sealed class EngineRoot
{
    private PlayerManager _playerManager;
    public IPlayerManager PlayerManager => _playerManager;
    internal GameStateObservable GameStateObservable { get; } = new();
    internal GameStateManager GameStateManager { get; } = new();
    private readonly ActionPerformer _actionPerformer = new();
    public IActionPerformer ActionPerformer => _actionPerformer;
    private readonly ActionStack _actionStack  = new();
    public IActionStack ActionStack => _actionStack;
    private readonly EntityManager _entityManager = new EntityManager();
    public IEntityManager EntityManager => _entityManager;
    
    private readonly InteractionManager _interactionManager = new();
    public IInteractionManager InteractionManager => _interactionManager;
    
    private readonly PhaseManager _phaseManager;
    public IPhaseManager PhaseManager => _phaseManager;

    public EngineRoot(HashSet<PlayerId> players, IPhase phase)
    {
        _playerManager = new PlayerManager(players);
        _phaseManager = new PhaseManager(phase);
        _initialize();
    }

    private void _initialize()
    {
        GameStateObservable.initialize(this);
        _actionPerformer.initialize(this);
        _actionStack.initialize(this);
        GameStateManager.initialize(this);
        _entityManager.initialize(this);
        _interactionManager.initialize(this);
        _phaseManager.initialize(this);
    }
}

internal interface IInitializeWithEngineRoot
{
    void  initialize(EngineRoot engineRoot);
}

public class BadRootInitializationException(string message) : Exception(message)
{
    
}