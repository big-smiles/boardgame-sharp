using example_cardgame.Board;
using example_cardgame.Card;

namespace example_cardgame;

public interface ICardGameState
{
    public List<ICard> Cards { get; }
    public List<IBoardTile> BoardTiles { get;  }
}

public class CardGameState(List<ICard> cards, List<IBoardTile> boardTiles) : ICardGameState
{
    public List<ICard> Cards { get; } = cards;
    public List<IBoardTile> BoardTiles { get; } = boardTiles;
}