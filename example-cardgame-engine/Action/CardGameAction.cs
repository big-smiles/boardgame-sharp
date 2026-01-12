using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;

namespace example_cardgame.Action;


public delegate void DAction(ICardGameActionPerformer actionPerformer, HashSet<EntityId> entityIds);

public sealed class CardGameAction(DAction doAction, DAction undoAction): IAction
{
    
    public void Do(IActionPerformer actionPerformer, HashSet<EntityId> entityIds)
    {
        var gameActionPerformer = new CardGameActionPerformer(actionPerformer);
        doAction(gameActionPerformer, entityIds);
    }

    public void Undo(IActionPerformer actionPerformer, HashSet<EntityId> entityIds)
    { 
        var gameActionPerformer = new CardGameActionPerformer(actionPerformer);
        undoAction(gameActionPerformer, entityIds);
    }
}