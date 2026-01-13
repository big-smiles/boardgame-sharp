using boardgames_sharp.Entity;
using boardgames_sharp.Phases;
using boardgames_sharp.Stack;
using example_cardgame.Action;
using example_cardgame.Card;
using example_cardgame.NDeck;

namespace example_cardgame.Phases;

public class CreateInitialCardsOnDeckPhase(DeckData deckData):IPhase
{
    public bool Next(IActionStack actionStack)
    {
        actionStack.AddPhaseAction(new CardGameAction(_do, _undo));
        return false;
    }

    private void _do(ICardGameActionPerformer performer, ActionContext context)
    {
        
            performer.Deck.create_deck(deckData);
        
    }

    private void _undo(ICardGameActionPerformer performer, ActionContext context)
    {
        throw new NotImplementedException();
    }
}