using boardgames_sharp.Entity;

namespace boardgames_sharp.GameState;

public class GameStateManager: IInitializeWithEngineRoot
{
    public void Initialize(EngineRoot engineRoot)
    {
        this._gameStateObservable = engineRoot.GameStateObservable;
        this._entityManager = engineRoot.EntityManager;
    }
    private GameStateObservable? _gameStateObservable;
    private IEntityManager? _entityManager;
    public GameStateManager()
    {}

    public void CreateNew()
    {
        if(_gameStateObservable == null) throw new NullReferenceException("Game state observable is null");
        if(_entityManager == null) throw new NullReferenceException("Entity manager is null");
        var entities = _entityManager.get_state();
        var gameState = new GameState(entities:entities);
        this._gameStateObservable.Publish(gameState);
    }
}