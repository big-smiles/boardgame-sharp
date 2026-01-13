using example_cardgame.Card;

namespace example_cardgame.NDeck;

public class DeckData
{
    public readonly IReadOnlyList<WeightedChoice<DeckData>>? Decks;
    public readonly IReadOnlyList<WeightedChoice<CardData>>? Cards;

    public DeckData(IReadOnlyList<WeightedChoice<DeckData>>? decks)
    {
        Cards = null;
        Decks = decks;
    }

    public DeckData(IReadOnlyList<WeightedChoice<CardData>>? cards)
    {
        Cards = cards;
        Decks = null;
    }
}

public record struct WeightedChoice<T>(T Value, float Weight, int AmountAvailable)
{
    public T Value = Value;
    public float Weight = Weight;
    public int AmountAvailable = AmountAvailable;
}

