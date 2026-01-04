using boardgames_sharp.Actions.ActionPerformer;

namespace boardgames_sharp.Actions;

public interface IAction
{
    void Do(IActionPerformer actionPerformer);
    void Undo(IActionPerformer actionPerformer);
}

