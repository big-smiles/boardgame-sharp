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
public sealed partial class CardActionPerformer(ICardGameActionPerformer performer): ICardActionPerformer
{
    private ICard _create_card(ICardData cardData)
    {
        var entity = performer.BasePerformer.Entity.create_entity();
        performer.BasePerformer.Entity.add_property<int>(entity.Id, CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE, CONSTANTS.ENTITY_TYPES.CARD);
        performer.BasePerformer.Entity.add_property<int>(entity.Id, CONSTANTS.PROPERTY_IDS.INT.CARD_LOCATION, (int)ECardLocations.Undefined);
        performer.BasePerformer.Entity.add_property(entity.Id, CONSTANTS.PROPERTY_IDS.STRING.CARD_NAME, cardData.Name);

        performer.BasePerformer.Entity.add_property(entity.Id, CONSTANTS.PROPERTY_IDS.BOOL.HAS_CAN_BE_PLAYED,
            cardData.CanBePlayed != null);
        if (cardData.CanBePlayed != null)
        {
            performer.BasePerformer.Entity.add_property(entity.Id, CONSTANTS.PROPERTY_IDS.ACTION.CARD_ONPLAY,
                cardData.CanBePlayed.OnPlay);
            performer.BasePerformer.Entity.add_property(entity.Id, CONSTANTS.PROPERTY_IDS.INT.CAN_BE_PLAYED_COOLDOWN,
                cardData.CanBePlayed.Cooldown);
        }

        return new example_cardgame.Card.Card(entity);
    }

    public ICard create_card_on_player_deck(ICardData cardData)
    {
        var card = _create_card(cardData);
        var deckId = performer.BasePerformer.Entity.get_saved_entity(CONSTANTS.KEY_ENTITY_IDS.PLAYER_DECK);
        performer.Container.add_entity_to_container(card.EntityId, deckId);
        performer.BasePerformer.Entity.add_modifier_set_value(card.EntityId, CONSTANTS.PROPERTY_IDS.INT.CARD_LOCATION, (int)ECardLocations.Deck);
        return card;
    }

    public void set_card_location(EntityId entityId, ECardLocations location)
    {
        performer.BasePerformer.Entity.add_modifier_set_value(entityId, CONSTANTS.PROPERTY_IDS.INT.CARD_LOCATION, (int)location);
    }
}