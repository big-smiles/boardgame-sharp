using boardgames_sharp.Entity;
using boardgames_sharp.GameState;
using example_cardgame.Card;
using example_cardgame.Constants;

namespace example_cardgame;


internal class GameStateObserver(IPublishCardGameState publisher):IObserver<IGameState>
{
    public void OnCompleted()
    {
        publisher.Complete();
    }

    public void OnError(Exception error)
    {
        publisher.Error(error);
    }

    public void OnNext(IGameState value)
    {
        List<ICard> cards = new List<ICard>();
        foreach (var tupleEntity in value.Entities.Entities)
        {
            var entity = tupleEntity.Item2;
            var card =  tryConvertEntityToCard(entity);
            if (card != null)
            {
                cards.Add(card);
            }
        }
        var cardGameState = new CardGameState(cards);
        publisher.Publish(cardGameState);
    }

    private ICard? tryConvertEntityToCard(StateEntity stateEntity)
    { 
        var entityType = stateEntity.IntProperties.Get(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE);
        if (entityType == CONSTANTS.ENTITY_TYPES.CARD)
        {
            return new Card.Card(stateEntity);
            
        }
        else
        {
            return null;
        }
    }
}