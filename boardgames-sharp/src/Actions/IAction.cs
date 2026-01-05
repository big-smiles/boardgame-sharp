using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;

namespace boardgames_sharp.Actions;

public interface IAction
{
    void Do(IActionPerformer actionPerformer, HashSet<EntityId> entityIds);
    void Undo(IActionPerformer actionPerformer, HashSet<EntityId> entityIds);
}