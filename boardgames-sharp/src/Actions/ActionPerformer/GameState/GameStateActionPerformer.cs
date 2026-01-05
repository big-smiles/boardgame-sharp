using boardgames_sharp;
using boardgames_sharp.GameState;

namespace boardgames_sharp.Actions.ActionPerformer.GameState;

public interface IGameStateActionPerformer
{
    void PublishNew();
}
internal sealed partial class GameStateActionPerformer: IGameStateActionPerformer, IInitializeWithEngineRoot
{
    public void initialize(EngineRoot engineRoot)
    {
        _gameStateManager = engineRoot.GameStateManager;
    }

    public void PublishNew()
    {
        if(_gameStateManager == null) throw new NullReferenceException("Game state manager is null");
        this._gameStateManager.CreateNew();
    }
    
    private GameStateManager? _gameStateManager;
}