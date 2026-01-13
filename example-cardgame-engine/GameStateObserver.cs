using boardgames_sharp.Entity;
using boardgames_sharp.GameState;
using example_cardgame.Board;
using example_cardgame.Card;
using example_cardgame.Constants;
using example_cardgame.NDeck;
using example_cardgame.PlayerCharacter;

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
        var playerCharacters = new List<IPlayerCharacter>();
        var decks = new List<IDeck>();
        foreach (var tupleEntity in value.Entities.Entities)
        {
            var entity = tupleEntity.Item2;
            
            var wasCard =  _tryConvertEntityToCard(entity, out var card);
            if (wasCard && card != null)
            {
                cards.Add(card);
                continue;
            }
            var wasBoardTile = _tryConvertEntityToBoardTile(entity, out var boardTile);
            if (wasBoardTile && boardTile != null)
            {
                boardTiles.Add(boardTile);
                continue;
            }

            var wasPlayerCharacter = _tryConverEntityToPlayerCharacter(entity,out var playerCharacter);
            if (wasPlayerCharacter && playerCharacter != null)
            {
                playerCharacters.Add(playerCharacter);
                continue;
            } 
            
            var wasDeck = _tryConverEntityToDeck(entity,out var deck);
            if (wasDeck && deck != null)
            {
                decks.Add(deck);
                continue;
            }
        }
        
        var cardGameState = new CardGameState(
            id: value.Id,
            cards: cards,
            boardTiles: boardTiles,
            playerCharacters: playerCharacters,
            decks: decks,
            winnerPlayerId: value.WinnerPlayerId,
            availableInteractions: value.AvailableInteractions
        );
        publisher.Publish(cardGameState);
    }

    private bool _tryConvertEntityToCard(StateEntity stateEntity,  out ICard? card)
    { 
        var entityTypeFound = stateEntity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE, out var entityType);
    
        if(!entityTypeFound || entityType != CONSTANTS.ENTITY_TYPES.CARD)
        {
            card =  null;
            return false;
        }
        card = new Card.Card(stateEntity);
        return true;
    }
    private bool  _tryConvertEntityToBoardTile(StateEntity stateEntity, out IBoardTile?  boardTile)
    {
        var foundEntityType = stateEntity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE, out var entityType);
        if (!foundEntityType || entityType != CONSTANTS.ENTITY_TYPES.CONTAINER)
        {
            boardTile = null;
            return false;
        }
        
        var found = stateEntity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE,
            out var containterType);
        if (!found || containterType != CONSTANTS.CONTAINER_TYPES.BOARD_TILE)
        {
            boardTile = null;
            return false;
        }
        
        boardTile = new BoardTile(stateEntity);
        return true;
    }

    private bool _tryConverEntityToPlayerCharacter(StateEntity stateEntity, out IPlayerCharacter? playerCharacter)
    {
        var entityTypeFound =
            stateEntity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE, out var entityType);
        if (!entityTypeFound || entityType != CONSTANTS.ENTITY_TYPES.PLAYER_CHARACTER)
        {
            playerCharacter = null;
            return false;
        }

        playerCharacter = new PlayerCharacter.PlayerCharacter(stateEntity);
        return true;
    }
    private bool _tryConverEntityToDeck(StateEntity stateEntity, out IDeck? deck)
    {
        var entityTypeFound =
            stateEntity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE, out var entityType);
        if (!entityTypeFound || entityType != CONSTANTS.ENTITY_TYPES.CONTAINER)
        {
            deck = null;
            return false;
        }
 
        var found = stateEntity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE,
            out var containterType);
        if (!found || containterType != CONSTANTS.CONTAINER_TYPES.CARD_DECK)
        {
            deck = null;
            return false;
        }

        deck = new Deck(stateEntity);
        return true;
    }
}