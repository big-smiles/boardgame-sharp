using System.ComponentModel.DataAnnotations;
using boardgames_sharp.Actions;
using boardgames_sharp.Entity;
using boardgames_sharp.Stack;

namespace boardgames_sharp.Interaction;

public interface IInteractionManager
{
    public void add_available_interaction(
        uint playerId,
        HashSet<EntityId> availableEntityIds,
        int min,
        int max,
        IAction  actionWhenSelected);
    public void clear_available_interactions();
    public void receive_interaction(Interaction interaction);
    public List<AvailableInteraction> get_available_interactions();
}
public class InteractionManager:IInteractionManager, IInitializeWithEngineRoot
{
   
    public void add_available_interaction(uint playerId, HashSet<EntityId> availableEntityIds, int min, int max, IAction actionWhenSelected)
    {
        var interactionId = NextId;
        var interactionOption = new AvailableInteraction(interactionId, playerId, availableEntityIds, min, max);
        _availableInteractions.Add(interactionId,new Tuple<AvailableInteraction, IAction>(interactionOption, actionWhenSelected));
    }   

    public void clear_available_interactions()
    {
        _availableInteractions.Clear();
    }

    public void receive_interaction(Interaction interaction)
    {
        if (_actionStack == null)
        {
            throw new BadRootInitializationException("_actionStack is null");
        }
        _check_interaction(interaction);
        var action = _availableInteractions[interaction.InteractionId].Item2;
        _actionStack.AddAction(action,interaction.SelectedEntityIds);
    }

    public List<AvailableInteraction> get_available_interactions()
    {
        var availableInteractions = new List<AvailableInteraction>();
        foreach (var interaction in _availableInteractions.Values)
        {
            availableInteractions.Add(interaction.Item1);
        }
        return availableInteractions;
    }

    private void _check_interaction(Interaction interaction)
    {
        var interactionId = interaction.InteractionId;
        if (!_availableInteractions.TryGetValue(interactionId, out var interactionTuple))
        {
            throw new InvalidInteraction("No interaction with id " + interactionId);
        }

        var availableInteraction = interactionTuple.Item1;
        
        if (interaction.SelectedEntityIds.Count < availableInteraction.Min)
        {
            throw new InvalidInteraction(
                "Selected " + interaction.SelectedEntityIds.Count + ", the min was" + availableInteraction.Min
            );
        }

        if (interaction.SelectedEntityIds.Count >= availableInteraction.Max)
        {
            throw new InvalidInteraction(
                "Selected " + interaction.SelectedEntityIds.Count + ", the max was" + availableInteraction.Max
            );  
        }

        if (!interaction.SelectedEntityIds.IsSubsetOf(availableInteraction.AvailableEntityIds))
        {
            throw new InvalidInteraction("Selected entities was not a subset of availableEntities");
        }
    }
    public void initialize(EngineRoot engineRoot)
    {
        _actionStack = engineRoot.ActionStack;
    }
    private ulong NextId
    {
        get => field++;
    } = 1;
    private Dictionary<ulong, Tuple<AvailableInteraction, IAction>> _availableInteractions = new();
    private ActionStack? _actionStack;

 
}

