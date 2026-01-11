using example_cardgame;
using example_cardgame.Action;
using example_cardgame.Card;
using JetBrains.Annotations;
namespace cardgame_tests;

[TestClass]
[TestSubject(typeof(CardGameEngine))]
public sealed class CardGameEngineTest
{
    [TestMethod]
    public void CreateOneCardInDeck()
    {
        var cardname = "card1";
        var card1 = new CardData()
        {
            Name = cardname,
            OnPlay = new CardGameAction(
                ((performer, ids) => throw new NotImplementedException()),
                ((performer, ids) => throw new NotImplementedException()))
        };
        var cards = new List<ICardData>() { card1 };
        CardGameEngine engine = new CardGameEngine(cards);
        var count = 0;
        var observer = new TestObserver(
            () =>
            {
                throw new NotImplementedException();
            },
            (Exception exception) =>
            {
                throw exception;
            },
            (ICardGameState state) =>
            {
                count++;
                Assert.IsNotNull(state);
                Assert.IsNotNull(state.Cards);
                Assert.HasCount(1, state.Cards);
                var card = state.Cards[0];
                Assert.IsNotNull(card);
                Assert.AreEqual(cardname, card.Name);
                Assert.AreEqual(ECardLocations.Deck, card.CardLocation);
            }
        );
        engine.Observable.Subscribe(observer);
        engine.Start();
        Assert.AreEqual(1, count);
    }
    [TestMethod]
    public void CreateTwoCardsInDeck()
    {
        var cardname1 = "card1";
        var cardname2 = "card2";
        HashSet<string> names = new HashSet<string>(){ cardname1, cardname2 };
        var card1 = new CardData()
        {
            Name = cardname1,
            OnPlay = new CardGameAction(
                ((performer, ids) => throw new NotImplementedException()),
                ((performer, ids) => throw new NotImplementedException()))
        };
        var card2 = new CardData()
        {
            Name = cardname2,
            OnPlay = new CardGameAction(
                ((performer, ids) => throw new NotImplementedException()),
                ((performer, ids) => throw new NotImplementedException()))
        };
        var cards = new List<ICardData>() { card1, card2 };
        CardGameEngine engine = new CardGameEngine(cards);
        var count = 0;
        var observer = new TestObserver(
            () =>
            {
                throw new NotImplementedException();
            },
            (Exception exception) =>
            {
                throw exception;
            },
            (ICardGameState state) =>
            {
                count++;
                Assert.IsNotNull(state);
                Assert.IsNotNull(state.Cards);
                Assert.HasCount(2, state.Cards);
                var stateNames = new HashSet<string>();
                foreach (var card in state.Cards)
                {
                    Assert.AreEqual(ECardLocations.Deck, card.CardLocation);
                    stateNames.Add(card.Name);
                    Assert.Contains(card.Name,names);
                }
                Assert.HasCount(names.Count, stateNames);
            }
        );
        engine.Observable.Subscribe(observer);
        engine.Start();
        Assert.AreEqual(1, count);

    }
}