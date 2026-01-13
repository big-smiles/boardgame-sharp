using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.Stack;

namespace example_cardgame.Action;


public delegate void DAction(ICardGameActionPerformer actionPerformer, ActionContext context);

public sealed class CardGameAction(DAction doAction, DAction undoAction): IAction
{
    
    public void Do(IActionPerformer actionPerformer, ActionContext context)
    {
        var gameActionPerformer = new CardGameActionPerformer(actionPerformer);
        doAction(gameActionPerformer, context);
    }

    public void Undo(IActionPerformer actionPerformer, ActionContext context)
    { 
        var gameActionPerformer = new CardGameActionPerformer(actionPerformer);
        undoAction(gameActionPerformer, context);
    }
}