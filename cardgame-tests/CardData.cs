using example_cardgame.Action;
using example_cardgame.Card;

namespace cardgame_tests;

public class CardData:ICardData
{
    public string Name { get; set; }
    public CardGameAction OnPlay { get; set; }
}