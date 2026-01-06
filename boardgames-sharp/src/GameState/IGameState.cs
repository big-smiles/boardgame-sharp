using boardgames_sharp.Entity;
using boardgames_sharp.Interaction;

namespace boardgames_sharp.GameState;

public interface IGameState
{
    public int Id { get; }
    public StateEntities Entities { get; }
    public List<AvailableInteraction> AvailableInteractions { get; }
}

public class GameState(int Id, StateEntities entities, List<AvailableInteraction> availableInteractions) : IGameState
{
    public int Id { get; } = Id;
    public StateEntities Entities { get; } = entities;
    public List<AvailableInteraction> AvailableInteractions { get; } = availableInteractions;
}