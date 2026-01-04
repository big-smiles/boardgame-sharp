using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.GameState;
using boardgames_sharp.Stack;

namespace boardgames_sharp;

public class EngineRoot
{
    internal GameStateObservable GameStateObservable { get; } = new();
    internal GameStateManager GameStateManager { get; } = new();
    internal ActionPerformer ActionPerformer { get; } = new();
    internal ActionStack ActionStack { get; } = new();
    private readonly EntityManager _entityManager = new EntityManager();
    public IEntityManager EntityManager => _entityManager;

    public EngineRoot()
    {
        _initialize();
    }

    private void _initialize()
    {
        ActionPerformer.Initialize(this);
        ActionStack.Initialize(this);
        GameStateManager.Initialize(this);
        _entityManager.Initialize(this);
    }
}

public interface IInitializeWithEngineRoot
{
    void  Initialize(EngineRoot engineRoot);
}