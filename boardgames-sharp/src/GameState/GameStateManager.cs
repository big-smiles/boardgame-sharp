using boardgames_sharp.Entity;
using boardgames_sharp.Interaction;
using boardgames_sharp.Player;

namespace boardgames_sharp.GameState;

internal sealed class GameStateManager: IInitializeWithEngineRoot
{
    public void initialize(EngineRoot engineRoot)
    {
        this._gameStateObservable = engineRoot.GameStateObservable;
        this._entityManager = engineRoot.EntityManager;
        this._interactionManager = engineRoot.InteractionManager;
        _playerManager = engineRoot.PlayerManager;
    }
    private GameStateObservable? _gameStateObservable;
    private IEntityManager? _entityManager;
    private IInteractionManager? _interactionManager;
    private IPlayerManager? _playerManager;
    public GameStateManager()
    {}

    
    public void CreateNew()
    {
        if(_gameStateObservable == null) throw new NullReferenceException("Game state observable is null");
        if(_entityManager == null) throw new NullReferenceException("Entity manager is null");
        if(_interactionManager == null) throw new NullReferenceException("Interaction manager is null");
        var entities = _entityManager.get_state();
        var availableInteractions = _interactionManager.get_available_interactions();
        var winnerPlayerId = _playerManager.GetWinnerPlayerId();
        var gameState = new GameState(Count, entities:entities, availableInteractions, winnerPlayerId);
        this._gameStateObservable.Publish(gameState);
    }

    private int Count
    {
        get => field++;
    } = 1;
}