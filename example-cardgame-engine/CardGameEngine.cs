using boardgames_sharp;
using boardgames_sharp.Player;
using example_cardgame.Card;
using example_cardgame.Constants;
using example_cardgame.Phases;

namespace example_cardgame;

public class CardGameEngine
{
    public CardGameEngine(List<ICardData> playerDeck)
    {
        var phases = new InitializationPhase(playerDeck);
        var players = new HashSet<PlayerId>(){CONSTANTS.PLAYER_IDS.PLAYER};
        var engine = new Engine(players, phases);
        _observable = new CardGameStateObservable();
        GameStateObserver gameStateObserver = new GameStateObserver(_observable);
        engine.GameStateObservable.Subscribe(gameStateObserver);
        engine.Start();
    }

    private CardGameStateObservable _observable;
    public IObservable<ICardGameState> Observable => _observable;
}