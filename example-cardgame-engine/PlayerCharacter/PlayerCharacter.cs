using boardgames_sharp.Entity;
using boardgames_sharp.Player;
using example_cardgame.Constants;

namespace example_cardgame.PlayerCharacter;

public interface IPlayerCharacter
{
    int Health { get; }
}
internal class PlayerCharacter:IPlayerCharacter
{
    public int Health { get; }

    public PlayerCharacter(IEntityReadOnly entity)
    {
        if (!entity.IntProperties.Contains(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE))
        {
            throw new("Did not hae entityType");
        }
        var entityType = entity.IntProperties.get_read_only(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE).CurrentValue();
        if (entityType != CONSTANTS.ENTITY_TYPES.PLAYER_CHARACTER)
        {
            throw new Exception("was not type PLAYER_CHARACTER");
        }
        
        Health = entity.IntProperties.get_read_only(CONSTANTS.PROPERTY_IDS.INT.PLAYER_HEALTH).CurrentValue();
    }
    public PlayerCharacter(StateEntity  stateEntity)
    {
        var entityTypeFound = stateEntity.IntProperties.TryAndGet(
            CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE,
            out var entityType
        );
        if (!entityTypeFound)
        {
            throw new("Did not find entityType");
        }

        if (entityType != CONSTANTS.ENTITY_TYPES.PLAYER_CHARACTER)
        {
            throw new Exception("was not type PLAYER_CHARACTER");
        }

        Health = stateEntity.IntProperties.Get(CONSTANTS.PROPERTY_IDS.INT.PLAYER_HEALTH);

    }
}