using boardgames_sharp.Entity;

namespace boardgames_sharp.Interaction;

public class AvailableInteraction(ulong interactionId, uint playerId, HashSet<EntityId> availableEntityIds, int min, int max)
{
     public ulong InteractionId { get;} = interactionId;
     public uint PlayerId { get; } = playerId;
     public HashSet<EntityId> AvailableEntityIds { get; } = availableEntityIds;
     public int Min { get; } = min;
     public int Max { get; } = max;
}

