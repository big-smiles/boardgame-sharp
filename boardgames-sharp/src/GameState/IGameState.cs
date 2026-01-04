using boardgames_sharp.Entity;

namespace boardgames_sharp.GameState;

public interface IGameState
{
    public StateEntities Entities { get; }
}

public class GameState(StateEntities entities) : IGameState
{
    public StateEntities Entities { get; } = entities;
}