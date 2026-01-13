using boardgames_sharp.Entity;
using example_cardgame.Card;
using example_cardgame.Constants;

namespace example_cardgame.NDeck;

public interface IDeck
{
    EntityId Id { get; }
    public IReadOnlyList<WeightedChoice<EntityId>>? Decks { get; }
    public IReadOnlyList<WeightedChoice<EntityId>>? Cards { get; }
}

public class Deck:IDeck
{
    public EntityId Id { get; }
    public IReadOnlyList<WeightedChoice<EntityId>>? Decks { get; }
    public IReadOnlyList<WeightedChoice<EntityId>>? Cards { get; }
    
    public Deck(IEntityReadOnly entity)
    {
        Id = entity.Id;
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

        var foundEntityIds =
            entity.try_and_get_value_of_set(CONSTANTS.PROPERTY_IDS.ISET.ENTITY_ID.CONTAINER_CHILDREN, out var entityIds);
        if (!foundEntityIds || entityIds == null)
        {
            throw new Exception("did not have property for children");
        }
        
        var foundDeckType = entity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.DECK_TYPE, out var deckType);
        if(!foundDeckType || deckType == null){
            throw new Exception("did not have property deckType");
        }
        
        var foundAmounts = entity.DictOfEntityIdToIntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.IDICTIONARY.DECK_AMOUNT, out var amounts);
        if (!foundAmounts || amounts == null)
        {
            throw new Exception("did not have property for amounts");
        }
        
        var foundWeights = entity.DictOfEntityIdToFloatProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.IDICTIONARY.DECK_WEIGHT, out var weights);
        if (!foundWeights || weights == null)
        {
            throw new Exception("did not have property for weights");
        }
        var weightedChoices = new List<WeightedChoice<EntityId>>();
        foreach (var entityId in entityIds)
        {
            var foundAmount = amounts.TryGetValue(entityId, out var amount);
            if (!foundAmount)
            {
                throw new Exception("did not have for amount for entityId " + entityId.Id);
            }
            var foundWeight = weights.TryGetValue(entityId, out var weight);
            if (!foundWeight)
            {
                throw new Exception("did not have for weight for entityId " + entityId.Id);
            }
            var weightedChoice = new WeightedChoice<EntityId>(Value:entityId, Weight:weight, AmountAvailable:amount);
            weightedChoices.Add(weightedChoice);
        }

        if (deckType == CONSTANTS.DECK_TYPES.DECK_OF_CARDS)
        {
            Decks = weightedChoices;
            Cards = null;
        }else if (deckType == CONSTANTS.DECK_TYPES.DECK_OF_DECKS)
        {
            Decks = null;
            Cards = weightedChoices;
        }
        else
        {
            throw new Exception("invalid deck type=" + deckType);
        }
    }

    public Deck(StateEntity entity)
    {
        Id = entity.EntityId;
        var foundEntityType =
            entity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE, out var entityType);
        if (!foundEntityType || entityType != CONSTANTS.ENTITY_TYPES.CONTAINER)
        {
            throw new Exception("was not type container");
        }
        
        var foundContainerType = entity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE, out var containerType);
        if (!foundContainerType || containerType != CONSTANTS.CONTAINER_TYPES.CARD_DECK)
        {
            throw new Exception("was not type card deck");
        }

        var foundEntityIds =
            entity.SetsOfEntityId.TryAndGet(CONSTANTS.PROPERTY_IDS.ISET.ENTITY_ID.CONTAINER_CHILDREN, out var entityIds);
        if (!foundEntityIds || entityIds == null)
        {
            throw new Exception("did not have property for children");
        }
        
        var foundDeckType = entity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.DECK_TYPE, out var deckType);
        if(!foundDeckType || deckType == null){
            throw new Exception("did not have property deckType");
        }
        
        var foundAmounts = entity.DictOfEntityIdToIntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.IDICTIONARY.DECK_AMOUNT, out var amounts);
        if (!foundAmounts || amounts == null)
        {
            throw new Exception("did not have property for amounts");
        }
        
        var foundWeights = entity.DictOfEntityIdToFloatProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.IDICTIONARY.DECK_WEIGHT, out var weights);
        if (!foundWeights || weights == null)
        {
            throw new Exception("did not have property for weights");
        }
        var weightedChoices = new List<WeightedChoice<EntityId>>();
        foreach (var entityId in entityIds)
        {
            var foundAmount = amounts.TryGetValue(entityId, out var amount);
            if (!foundAmount)
            {
                throw new Exception("did not have for amount for entityId " + entityId.Id);
            }
            var foundWeight = weights.TryGetValue(entityId, out var weight);
            if (!foundWeight)
            {
                throw new Exception("did not have for weight for entityId " + entityId.Id);
            }
            var weightedChoice = new WeightedChoice<EntityId>(Value:entityId, Weight:weight, AmountAvailable:amount);
            weightedChoices.Add(weightedChoice);
        }

        if (deckType == CONSTANTS.DECK_TYPES.DECK_OF_CARDS)
        {
            Decks = weightedChoices;
            Cards = null;
        }else if (deckType == CONSTANTS.DECK_TYPES.DECK_OF_DECKS)
        {
            Decks = null;
            Cards = weightedChoices;
        }
        else
        {
            throw new Exception("invalid deck type=" + deckType);
        }
    }
}