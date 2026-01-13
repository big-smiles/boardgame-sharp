using boardgames_sharp.Entity;
using boardgames_sharp.Phases;
using boardgames_sharp.Stack;
using example_cardgame.Action;
using example_cardgame.Card;
using example_cardgame.Constants;
using example_cardgame.NDeck;

namespace example_cardgame.Phases;

public class InitializationPhase:IPhase
{
    public InitializationPhase(DeckData deckData, int board_columns, int board_rows)
    {
        var create_initial_cards_on_deck = new CreateInitialCardsOnDeckPhase(deckData);
        var create_board = new CreateBoardPhase(board_columns, board_rows);
        _phaseGroup = new PhaseGroup([
            create_initial_cards_on_deck,
            create_board
        ], false);
    }

    private PhaseGroup _phaseGroup;

    public bool Next(IActionStack actionStack)
    {
        var pending = _phaseGroup.Next(actionStack);
        if (pending)
        {
            return pending;
        }
        actionStack.AddPhaseAction(new CardGameAction(_do, _undo));
        return false;
    }

    private void _do(ICardGameActionPerformer performer, ActionContext context)
    {
        performer.BasePerformer.GameState.PublishNew();
    }

    private void _undo(ICardGameActionPerformer performer, ActionContext context)
    {
        throw new NotImplementedException();  
    }

}