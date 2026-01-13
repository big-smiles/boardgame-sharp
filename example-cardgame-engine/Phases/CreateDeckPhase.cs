using boardgames_sharp.Entity;
using boardgames_sharp.Phases;
using boardgames_sharp.Stack;
using example_cardgame.Action;

namespace example_cardgame.Phases;

public class CreateDeckPhase():IPhase
{
    public bool Next(IActionStack actionStack)
    {
        actionStack.AddPhaseAction(new CardGameAction(_do, _undo));
        return false;
    }

    private void _do(ICardGameActionPerformer performer, HashSet<EntityId> entityIds)
    {
        performer.Deck.create_player_deck();
    }

    private void _undo(ICardGameActionPerformer performer, HashSet<EntityId> entityIds)
    {
        throw new NotImplementedException();
    }
}
