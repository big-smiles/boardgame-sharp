using boardgames_sharp.Entity;
using boardgames_sharp.Phases;
using boardgames_sharp.Stack;
using example_cardgame.Action;
using example_cardgame.Card;
using example_cardgame.Constants;

namespace example_cardgame.Phases;

public class CreateBoardPhase(int boardColumns, int boardRows):IPhase
{
    public bool Next(IActionStack actionStack)
    {
        actionStack.AddPhaseAction(new CardGameAction(_do, _undo));
        return false;
    }
    private void _do(ICardGameActionPerformer performer, ActionContext context)
    {
        performer.Board.create_board();
        for (var x = 0; x < boardColumns; x++){
            for (var y = 0; y < boardRows; y++)
            {
                performer.Board.create_board_tile(x, y);  
            }
        }    
    }
    private void _undo(ICardGameActionPerformer performer, ActionContext context)
    {
        throw new NotImplementedException();
    }
}
