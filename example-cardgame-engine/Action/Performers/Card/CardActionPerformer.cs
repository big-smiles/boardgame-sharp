using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using example_cardgame.Card;
using example_cardgame.Constants;

namespace example_cardgame.Action.Card;

public interface ICardActionPerformer
{
    ICard create_card_on_player_deck(ICardData cardData);
    internal void set_card_location(EntityId entityId, ECardLocations location);
}
public sealed partial class CardActionPerformer(IActionPerformer basePerformer, ICardGameActionPerformer cardGamePerformer): ICardActionPerformer
{
    private ICard _create_card(ICardData cardData)
    {
        var entity = basePerformer.Entity.create_entity();
        basePerformer.Entity.add_property<int>(entity.Id, CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE, CONSTANTS.ENTITY_TYPES.CARD);
        basePerformer.Entity.add_property<int>(entity.Id, CONSTANTS.PROPERTY_IDS.INT.CARD_LOCATION, (int)ECardLocations.Undefined);
        basePerformer.Entity.add_property<IAction?>(entity.Id, CONSTANTS.PROPERTY_IDS.ACTION.CARD_ONPLAY, cardData.OnPlay);
        basePerformer.Entity.add_property(entity.Id, CONSTANTS.PROPERTY_IDS.STRING.CARD_NAME, cardData.Name);
        return new example_cardgame.Card.Card(entity);
    }

    public ICard create_card_on_player_deck(ICardData cardData)
    {
        var card = _create_card(cardData);
        var deckId = basePerformer.Entity.get_saved_entity(CONSTANTS.KEY_ENTITY_IDS.PLAYER_DECK);
        cardGamePerformer.Container.add_entity_to_container(card.EntityId, deckId);
        cardGamePerformer.BasePerformer.Entity.add_modifier_set_value(card.EntityId, CONSTANTS.PROPERTY_IDS.INT.CARD_LOCATION, (int)ECardLocations.Deck);
        return card;
    }

    public void set_card_location(EntityId entityId, ECardLocations location)
    {
        basePerformer.Entity.add_modifier_set_value(entityId, CONSTANTS.PROPERTY_IDS.INT.CARD_LOCATION, (int)location);
    }
}