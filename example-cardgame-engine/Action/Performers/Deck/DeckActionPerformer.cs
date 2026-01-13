using example_cardgame.Constants;
using example_cardgame.NDeck;

namespace example_cardgame.Action;

public interface IDeckActionPerformer
{
    IDeck create_player_deck();
    IDeck? create_discard_deck();
}
public class DeckActionPerformer(ICardGameActionPerformer performer):IDeckActionPerformer
{
    public IDeck create_player_deck()
    {
        var playerDeck = performer.Container.create_container();
        performer.BasePerformer.Entity.add_property(
            playerDeck.Id,
            CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE,
            CONSTANTS.CONTAINER_TYPES.CARD_DECK
        );
        performer.BasePerformer.Entity.save_entity(playerDeck.Id, CONSTANTS.KEY_ENTITY_IDS.PLAYER_DECK);
        return new Deck(playerDeck);
    }

    public IDeck? create_discard_deck()
    {
        var playerDeck = performer.Container.create_container();
        performer.BasePerformer.Entity.add_property(
            playerDeck.Id,
            CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE,
            CONSTANTS.CONTAINER_TYPES.CARD_DECK
        );
        performer.BasePerformer.Entity.save_entity(playerDeck.Id, CONSTANTS.KEY_ENTITY_IDS.PLAYER_DISCARD);
        return new Deck(playerDeck);    
    }
}