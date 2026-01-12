using boardgames_sharp.Entity;
using boardgames_sharp.GameState;
using example_cardgame.Board;
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
        var cards = new List<ICard>();
        var boardTiles = new List<IBoardTile>();
        foreach (var tupleEntity in value.Entities.Entities)
        {
            var entity = tupleEntity.Item2;
            var card =  _tryConvertEntityToCard(entity);
            if (card != null)
            {
                cards.Add(card);
            }
            var boardTile = _tryConvertEntityToBoardTile(entity);
            if (boardTile != null)
            {
                boardTiles.Add(boardTile);
            }
        }
        var cardGameState = new CardGameState(cards, boardTiles);
        publisher.Publish(cardGameState);
    }

    private ICard? _tryConvertEntityToCard(StateEntity stateEntity)
    { 
        var entityType = stateEntity.IntProperties.Get(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE);
        if (entityType == CONSTANTS.ENTITY_TYPES.CARD)
        {
            return (ICard) new Card.Card(stateEntity);
            
        }
        else
        {
            return null;
        }
    }
    private IBoardTile? _tryConvertEntityToBoardTile(StateEntity stateEntity)
    { 
        var entityType = stateEntity.IntProperties.Get(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE);
        if (entityType == CONSTANTS.ENTITY_TYPES.CONTAINER)
        {
            var found = stateEntity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE,
                out var containterType);
            if (!found)
            {
                return null;
            }

            if (containterType != CONSTANTS.CONTAINER_TYPES.BOARD_TILE)
            {
                return null;
            }
            
            return new BoardTile(stateEntity);
        }
        else
        {
            return null;
        }
    }
}