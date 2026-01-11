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
        _engine = new Engine(players, phases);
        _observable = new CardGameStateObservable();
        GameStateObserver gameStateObserver = new GameStateObserver(_observable);
        _engine.GameStateObservable.Subscribe(gameStateObserver);
    }

    public void Start()
    {
        _engine.Start();
    }
    
    private readonly Engine _engine;
    private readonly CardGameStateObservable _observable;
    public IObservable<ICardGameState> Observable => _observable;
}