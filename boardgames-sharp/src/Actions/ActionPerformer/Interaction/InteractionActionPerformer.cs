using System.Reflection.Metadata;
using boardgames_sharp.Entity;
using boardgames_sharp.Interaction;

namespace boardgames_sharp.Actions.ActionPerformer.Interaction;

public interface IInteractionActionPerformer
{
    public void add_available_interaction(
        uint playerId,
        HashSet<EntityId> availableEntityIds,
        int min,
        int max,
        IAction  actionWhenSelected);
    public void clear_available_interactions();
}
internal sealed partial class InteractionActionPerformer: IInitializeWithEngineRoot, IInteractionActionPerformer
{
    public void initialize(EngineRoot engineRoot)
    {
        _interactionManager = engineRoot.InteractionManager;
    }
    
    /// <param name="min">minimun amount of entities that can be selectied, INCLUSIVE.</param>
    /// <param name="max">maximun amount of entities that can be selected, EXCLUSIVE.</param>
    public void add_available_interaction(uint playerId, HashSet<EntityId> availableEntityIds, int min, int max, IAction actionWhenSelected)
    {
        if (_interactionManager == null)
        {
            throw new BadRootInitializationException("Interaction Manager not initialized");
        }
        _interactionManager.add_available_interaction(playerId, availableEntityIds, min, max, actionWhenSelected);
    }

    public void clear_available_interactions()
    {
        if (_interactionManager == null)
        {
            throw new BadRootInitializationException("Interaction Manager not initialized");
        }
        _interactionManager.clear_available_interactions();
    }
    private IInteractionManager? _interactionManager;
}