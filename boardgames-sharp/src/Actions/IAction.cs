using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.Player;
using boardgames_sharp.Stack;

namespace boardgames_sharp.Actions;

public interface IAction
{
    void Do(IActionPerformer actionPerformer, ActionContext actionContext);
    void Undo(IActionPerformer actionPerformer, ActionContext actionContext);
}