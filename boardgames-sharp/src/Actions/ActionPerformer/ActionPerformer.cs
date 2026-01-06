using boardgames_sharp.Actions.ActionPerformer.Entity;
using boardgames_sharp.Actions.ActionPerformer.GameState;
using boardgames_sharp.Actions.ActionPerformer.Interaction;
using boardgames_sharp.Actions.ActionPerformer.Phase;

namespace boardgames_sharp.Actions.ActionPerformer;

public interface IActionPerformer
{
    IEntityActionPerformer Entity { get; }
    IGameStateActionPerformer GameState { get; }
    IInteractionActionPerformer Interaction { get; }
    IPhaseActionPerformer Phase { get; }
}
internal sealed class ActionPerformer: IActionPerformer, IInitializeWithEngineRoot
{
    public void initialize(EngineRoot engineRoot)
    {
        this._entity.initialize(engineRoot);
        this._gameState.initialize(engineRoot);
        this._interaction.initialize(engineRoot);
        _phase.initialize(engineRoot);
    }

    private readonly EntityActionPerformer _entity = new();
    public IEntityActionPerformer Entity => _entity;
    private readonly GameStateActionPerformer _gameState = new();
    public IGameStateActionPerformer GameState => _gameState;
    private readonly InteractionActionPerformer _interaction = new();
    public IInteractionActionPerformer Interaction => _interaction;
    private readonly PhaseActionPerformer _phase = new();
    public IPhaseActionPerformer Phase => _phase;
}