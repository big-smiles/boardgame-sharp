using example_cardgame.Constants;
using example_cardgame.NDeck;

namespace example_cardgame.Action;

public interface IDeckActionPerformer
{
    IDeck create_deck(DeckData data);
    IDeck? create_discard_deck();
}
public class DeckActionPerformer(ICardGameActionPerformer performer):IDeckActionPerformer
{
    public IDeck create_deck(DeckData data)
    {
        var playerDeck = performer.Container.create_container();
        performer.BasePerformer.Entity.add_property(
            playerDeck.Id,
            CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE,
            CONSTANTS.CONTAINER_TYPES.CARD_DECK
        );
        performer.BasePerformer.Entity.add_property_of_dictionary(playerDeck.Id, CONSTANTS.PROPERTY_IDS.IDICTIONARY.DECK_AMOUNT);
        performer.BasePerformer.Entity.add_property_of_dictionary(playerDeck.Id, CONSTANTS.PROPERTY_IDS.IDICTIONARY.DECK_WEIGHT);
        
        if (data.Cards == null && data.Decks == null)
        {
            throw new Exception("both cards and decks were null");
        }

        if (data.Cards != null && data.Decks != null)
        {
            throw new Exception("both cards and decks were NOT null");
        }
       
        if (data.Cards != null)
        {
            performer.BasePerformer.Entity.add_property(playerDeck.Id, CONSTANTS.PROPERTY_IDS.INT.DECK_TYPE,
                CONSTANTS.DECK_TYPES.DECK_OF_CARDS);
           
            
            foreach (var cardData in data.Cards)
            {
                var card = performer.Card.create_card_on_deck(cardData.Value, playerDeck.Id);
                performer.BasePerformer.Entity.PropertiesOfDicitonary.add_element(playerDeck.Id, CONSTANTS.PROPERTY_IDS.IDICTIONARY.DECK_AMOUNT, card.EntityId, cardData.AmountAvailable);
                performer.BasePerformer.Entity.PropertiesOfDicitonary.add_element(playerDeck.Id, CONSTANTS.PROPERTY_IDS.IDICTIONARY.DECK_WEIGHT, card.EntityId, cardData.Weight);
                
            }
        }
        else if(data.Decks != null) 
        {
            performer.BasePerformer.Entity.add_property(playerDeck.Id, CONSTANTS.PROPERTY_IDS.INT.DECK_TYPE,
                CONSTANTS.DECK_TYPES.DECK_OF_DECKS);
            foreach (var deckData in data.Decks)
            {
                var deck = performer.Deck.create_deck(deckData.Value);
                performer.Container.add_entity_to_container(deck.Id, playerDeck.Id);
                performer.BasePerformer.Entity.PropertiesOfDicitonary.add_element(playerDeck.Id, CONSTANTS.PROPERTY_IDS.IDICTIONARY.DECK_AMOUNT, deck.Id, deckData.AmountAvailable);
                performer.BasePerformer.Entity.PropertiesOfDicitonary.add_element(playerDeck.Id, CONSTANTS.PROPERTY_IDS.IDICTIONARY.DECK_WEIGHT, deck.Id, deckData.Weight);
                
            }
        }
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