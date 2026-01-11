using example_cardgame.Card;

namespace example_cardgame;

public interface ICardGameState
{
    public List<ICard> Cards { get; }
}

public class CardGameState(List<ICard> cards) : ICardGameState
{
    public List<ICard> Cards { get; } = cards;
}