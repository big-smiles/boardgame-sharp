using boardgames_sharp.Actions.ActionPerformer;
using example_cardgame.Action.Board;
using example_cardgame.Action.Card;
using example_cardgame.Action.Container;
using example_cardgame.Action.PlayerCharacter;

namespace example_cardgame.Action;

public interface ICardGameActionPerformer
{
    public IActionPerformer BasePerformer { get; }
    public IContainerActionPerformer Container { get; }
    public ICardActionPerformer Card { get; }
    public IBoardActionPerformer Board { get; }
    public IPlayerCharacterActionPerformer PlayerCharacter { get; }
    public IDeckActionPerformer Deck { get; }
}

internal class CardGameActionPerformer : ICardGameActionPerformer
{
    public CardGameActionPerformer(IActionPerformer basePerformer)
    {
        BasePerformer = basePerformer;
        _container = new ContainerActionPerformer(this);
        _card = new CardActionPerformer(this);
        _board = new BoardActionPerformer(this);
        _playerCharacter = new PlayerCharacterActionPerformer(this);
        _deck = new DeckActionPerformer(this);
    }
    public IActionPerformer BasePerformer { get; }
    
    private readonly ContainerActionPerformer _container;
    public IContainerActionPerformer Container => _container;
    
    private readonly CardActionPerformer _card;
    public ICardActionPerformer Card => _card;
    
    private readonly BoardActionPerformer _board;
    public IBoardActionPerformer Board => _board;
    
    private readonly PlayerCharacterActionPerformer _playerCharacter;
    public IPlayerCharacterActionPerformer PlayerCharacter => _playerCharacter;
    public readonly DeckActionPerformer _deck;
    public IDeckActionPerformer Deck => _deck;
}

public class InvalidEntityType(string message) : Exception(message)
{
    
}