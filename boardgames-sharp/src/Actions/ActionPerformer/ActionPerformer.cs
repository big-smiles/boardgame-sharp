using boardgames_sharp.Actions.ActionPerformer.Entity;
using boardgames_sharp.Actions.ActionPerformer.GameState;

namespace boardgames_sharp.Actions.ActionPerformer;

public interface IActionPerformer
{
    IEntityActionPerformer Entity { get; }
    IGameStateActionPerformer GameState { get; }
    
}
internal sealed class ActionPerformer: IActionPerformer, IInitializeWithEngineRoot
{
    public void Initialize(EngineRoot engineRoot)
    {
        this._entity.Initialize(engineRoot);
        this._gameState.Initialize(engineRoot);
    }

    private readonly EntityActionPerformer _entity = new();
    public IEntityActionPerformer Entity => _entity;
    private readonly GameStateActionPerformer _gameState = new();
    public IGameStateActionPerformer GameState => _gameState;
}