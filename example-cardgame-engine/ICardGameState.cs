using boardgames_sharp.Interaction;
using boardgames_sharp.Player;
using example_cardgame.Board;
using example_cardgame.Card;
using example_cardgame.NDeck;
using example_cardgame.PlayerCharacter;

namespace example_cardgame;

public interface ICardGameState
{
    public int Id { get; }
    public List<AvailableInteraction> AvailableInteractions { get; }
    public PlayerId? WinnerPlayerId { get; }
    public List<ICard> Cards { get; }
    public List<IBoardTile> BoardTiles { get;  }
    public List<IPlayerCharacter> PlayerCharacters { get; }
    public List<IDeck> Decks { get; }
    
}

public class CardGameState(int id,List<ICard> cards, List<IBoardTile> boardTiles, List<IPlayerCharacter> playerCharacters, List<IDeck> decks, PlayerId? winnerPlayerId, List<AvailableInteraction> availableInteractions) : ICardGameState
{
    public int Id { get; } = id;
    public List<AvailableInteraction> AvailableInteractions { get; } = availableInteractions;
    public PlayerId? WinnerPlayerId { get; } = winnerPlayerId;
    public List<ICard> Cards { get; } = cards;
    public List<IBoardTile> BoardTiles { get; } = boardTiles;
    public List<IPlayerCharacter> PlayerCharacters { get; } = playerCharacters;
    public List<IDeck> Decks { get; } = decks;
}