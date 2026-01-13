using boardgames_sharp.Actions;
using boardgames_sharp.Entity;
using example_cardgame.Action;
using example_cardgame.Action.Card;
using example_cardgame.Constants;

namespace example_cardgame.Card;

public enum ECardLocations
{
    Undefined=0, Deck=1, Board=2, Discard=3, Hand=4, Exile=5
}
public interface ICard
{
    string Name { get; }
    EntityId EntityId { get; }
    public ECardLocations CardLocation { get; }
    ICanBePlayed? CanBePlayed { get; }
}

public interface ICardData
{
    string Name{ get; }
    ICanBePlayedData? CanBePlayed { get; }
}
public class CardData:ICardData
{
    public string Name { get; set; }
    public ICanBePlayedData? CanBePlayed { get; set; }
}

public class Card:ICard
{
    public Card(IEntityReadOnly entityReadOnly)
    {
        EntityId = entityReadOnly.Id;

        var intProperties = entityReadOnly.get_readonly_properties_of_type<int>();
        if (!intProperties.Contains(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE))
        {
            throw new InvalidEntityType("entity did not ahve property entity type");
        }
        var cardType = intProperties.get_read_only(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE).CurrentValue();  
        if (cardType != CONSTANTS.ENTITY_TYPES.CARD)
        {
            throw new InvalidEntityType("entity was not type card was=" + cardType);
        }
        
        CardLocation = (ECardLocations) intProperties.get_read_only(CONSTANTS.PROPERTY_IDS.INT.CARD_LOCATION).CurrentValue();
        
        var stringProperties = entityReadOnly.get_readonly_properties_of_type<string>();
        Name = stringProperties.get_read_only(CONSTANTS.PROPERTY_IDS.STRING.CARD_NAME).CurrentValue();
    }
    public Card(StateEntity stateEntity)
    {
        EntityId = stateEntity.EntityId;
        CardLocation = (ECardLocations) stateEntity.IntProperties.Get(CONSTANTS.PROPERTY_IDS.INT.CARD_LOCATION);
        Name = stateEntity.StringProperties.Get(CONSTANTS.PROPERTY_IDS.STRING.CARD_NAME);
    }
    public string Name { get; }
    public EntityId EntityId { get; }
    public ECardLocations CardLocation { get; }
    public ICanBePlayed? CanBePlayed { get; }
}

