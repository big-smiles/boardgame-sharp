using boardgames_sharp.Entity;

namespace boardgames_sharp.Interaction;

public class Interaction(ulong interactionId, IReadOnlySet<EntityId> selectedEntityIds)
{
    public ulong InteractionId { get;} = interactionId;
    public IReadOnlySet<EntityId> SelectedEntityIds { get; } = selectedEntityIds;
}

public class InvalidInteraction(string message) : Exception(message);