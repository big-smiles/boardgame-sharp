using boardgames_sharp.Actions.ActionPerformer;
using example_cardgame.Action.Board;
using example_cardgame.Action.Card;
using example_cardgame.Action.Container;

namespace example_cardgame.Action;

public interface ICardGameActionPerformer
{
    public IActionPerformer BasePerformer { get; }
    public IContainerActionPerformer Container { get; }
    public ICardActionPerformer Card { get; }
    public IBoardActionPerformer Board { get; }
}

internal class CardGameActionPerformer : ICardGameActionPerformer
{
    public IActionPerformer BasePerformer { get; }
  

    private readonly ContainerActionPerformer _container;
    public IContainerActionPerformer Container => _container;
    private readonly CardActionPerformer _card;
    

    public CardGameActionPerformer(IActionPerformer basePerformer)
    {
        BasePerformer = basePerformer;
        _container = new ContainerActionPerformer(basePerformer);
        _card = new CardActionPerformer(basePerformer, this);
        _board = new BoardActionPerformer(basePerformer, this);
    }

    public ICardActionPerformer Card => _card;
    private readonly BoardActionPerformer _board;
    public IBoardActionPerformer Board => _board;
}

public class InvalidEntityType(string message) : Exception(message)
{
    
}