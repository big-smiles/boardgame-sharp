using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;
using boardgames_sharp.Phases;
using boardgames_sharp.Stack;
using example_cardgame;
using example_cardgame.Action;
using example_cardgame.Card;
using example_cardgame.Constants;
using example_cardgame.NDeck;
using example_cardgame.Phases;
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
        var card1 = new WeightedChoice<CardData>( new CardData()
        {
            Name = cardname,
        },
            1,1);
        IReadOnlyList<WeightedChoice<CardData>> cards = new List<WeightedChoice<CardData>>() { card1 };
        var deckData = new DeckData(cards);
        var phases = new InitializationPhase(deckData,0,0);
        CardGameEngine engine = new CardGameEngine(phases);
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
        var card1 = new WeightedChoice<CardData>(new CardData()
        {
            Name = cardname1,
        },0.5f,1);
        var card2 = new WeightedChoice<CardData>(new CardData()
        {
            Name = cardname2,
        },0.5f,1);;
        IReadOnlyList<WeightedChoice<CardData>> cards = new List<WeightedChoice<CardData>>() { card1, card2 };
        var deckData = new DeckData(cards);
        var phases = new InitializationPhase(deckData,0,0);
        CardGameEngine engine = new CardGameEngine(phases);
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
    
    [TestMethod]
    public void CreateTwoCardsInDeckMoveOneToBoard()
    {
        var x = 1;
        var y = 1;
        EntityId? boardTileId = null; 
        var cardname1 = "card1";
        var cardname2 = "card2";
        HashSet<string> names = new HashSet<string>(){ cardname1, cardname2 };
        var card1 = new WeightedChoice<CardData>(new CardData()
        {
            Name = cardname1,
        }, 0.5f, 1);
        var card2 = new WeightedChoice<CardData>(new CardData()
        {
            Name = cardname2,
        }, 0.5f, 1);
        IReadOnlyList<WeightedChoice<CardData>> cards = new List<WeightedChoice<CardData>>() { card1, card2 };
        var deckData = new DeckData(cards);
        var intializationPhase = new InitializationPhase(deckData,3,3);

        var movePhase = new TestPhase(new CardGameAction(
            doAction:((performer, ids) =>
            {
                var boardTile = performer.Board.get_board_tile(x, y);
                boardTileId = boardTile.Id;
                var query = new EntityQuery((entity =>
                {
                    var nameFound = entity.try_and_get_value_of_type(CONSTANTS.PROPERTY_IDS.STRING.CARD_NAME, out var name);
                    if (!nameFound || name != cardname1)
                    {
                        return false;
                    }

                    return true;
                }));
                var cardIds = performer.BasePerformer.Entity.query_entity_ids(query);
                Assert.HasCount(1, cardIds);
                var cardId =  cardIds.First();
                performer.Board.move_card_to_board_tile(x, y, cardId);
                performer.BasePerformer.GameState.PublishNew();
                
            }),
            undoAction:((performer, ids) => throw new NotImplementedException())
        ));
        
        var phases = new PhaseGroup([intializationPhase, movePhase], false);
        CardGameEngine engine = new CardGameEngine(phases);
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
                if (count == 2)
                {
                    Assert.IsNotNull(state);
                    Assert.IsNotNull(state.Cards);
                    Assert.HasCount(2, state.Cards);
                    Assert.HasCount(9, state.BoardTiles);
                    var count1 = 0;
                    var count2 = 0;
                    EntityId? cardId = null;
                    foreach (var card in state.Cards)
                    {
                        if (card.Name == cardname1)
                        {
                            count1++;
                            Assert.AreEqual(ECardLocations.Board, card.CardLocation);
                            cardId = card.EntityId;
                        }
                        else if (card.Name == cardname2)
                        {
                            count2++;
                            Assert.AreEqual(ECardLocations.Deck, card.CardLocation);
                        }
                    }

                    Assert.AreEqual(1, count1);
                    Assert.AreEqual(1, count2);
                    Assert.IsNotNull(cardId);
                    var tile = state.BoardTiles.Find(tile => tile.X == x && tile.Y == y);
                    Assert.IsNotNull(tile);
                    Assert.IsNotNull(tile.Card);
                    Assert.AreEqual(cardId, tile.Card);
                }
            }
        );
        engine.Observable.Subscribe(observer);
        engine.Start();
        Assert.AreEqual(2, count);

    }
}

internal class TestPhase(CardGameAction action ): IPhase
{
    public bool Next(IActionStack actionStack)
    {
        actionStack.AddPhaseAction(action);
        return false;
    }
}