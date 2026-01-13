using boardgames_sharp.Entity;
using example_cardgame.Constants;

namespace example_cardgame.NDeck;

public interface IDeck
{
    IReadOnlySet<EntityId>  Cards { get; }
    
}
public class Deck:IDeck
{
    public IReadOnlySet<EntityId> Cards { get; }

    public Deck(IEntityReadOnly entity)
    {
        var foundEntityType =
            entity.try_and_get_value_of_type(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE, out var entityType);
        if (!foundEntityType || entityType != CONSTANTS.ENTITY_TYPES.CONTAINER)
        {
            throw new Exception("was not type container");
        }
        var foundContainerType = entity.try_and_get_value_of_type(CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE, out var containerType);
        if (!foundContainerType || containerType != CONSTANTS.CONTAINER_TYPES.CARD_DECK)
        {
            throw new Exception("was not type card deck");
        }

        var foundCards =
            entity.try_and_get_value_of_set(CONSTANTS.PROPERTY_IDS.ISET.ENTITY_ID.CONTAINER_CHILDREN, out var cards);
        if (!foundCards || cards == null)
        {
            throw new Exception("did not have property for children");
        }
        Cards = cards;
    }

    public Deck(StateEntity stateEntity)
    {
        var foundEntityType =
            stateEntity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE, out var entityType);
        if (!foundEntityType || entityType != CONSTANTS.ENTITY_TYPES.CONTAINER)
        {
            throw new Exception("was not type container");
        }
        var foundContainerType = stateEntity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE, out var containerType);
        if (!foundContainerType || containerType != CONSTANTS.CONTAINER_TYPES.CARD_DECK)
        {
            throw new Exception("was not type card deck");
        }

        var foundCards =
            stateEntity.SetsOfEntityId.TryAndGet(CONSTANTS.PROPERTY_IDS.ISET.ENTITY_ID.CONTAINER_CHILDREN, out var cards);
        if (!foundCards || cards == null)
        {
            throw new Exception("did not have property for children");
        }
        Cards = cards;
    }
}