using boardgames_sharp.Entity;
using boardgames_sharp.Phases;
using boardgames_sharp.Stack;
using example_cardgame.Action;
using example_cardgame.Card;

namespace example_cardgame.Phases;

public class CreateInitialCardsOnDeckPhase(List<ICardData> cardsOnPlayerDeck):IPhase
{
    public bool Next(IActionStack actionStack)
    {
        actionStack.AddPhaseAction(new CardGameAction(_do, _undo));
        return false;
    }

    private void _do(ICardGameActionPerformer performer, HashSet<EntityId> entityIds)
    {
        foreach (var cardData in cardsOnPlayerDeck)
        {
            performer.Card.create_card_on_player_deck(cardData);
        }
    }

    private void _undo(ICardGameActionPerformer performer, HashSet<EntityId> entityIds)
    {
        throw new NotImplementedException();
    }
}