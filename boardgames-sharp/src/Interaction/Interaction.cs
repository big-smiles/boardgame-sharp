using boardgames_sharp.Entity;

namespace boardgames_sharp.Interaction;

public class Interaction(ulong interactionId, HashSet<EntityId> selectedEntityIds)
{
    public ulong InteractionId { get;} = interactionId;
    public HashSet<EntityId> SelectedEntityIds { get; } = selectedEntityIds;
}

public class InvalidInteraction(string message) : Exception(message);