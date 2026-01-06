using System.Collections.Generic;
using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;

namespace boardgames_sharp.Tests;


internal class TestAction(DAction @do, DAction undo) : IAction
{
    public void Do(IActionPerformer actionPerformer, HashSet<EntityId>  ids)
    {
        @do(actionPerformer, ids);
    }

    public void Undo(IActionPerformer actionPerformer, HashSet<EntityId>  ids)
    {
        undo(actionPerformer, ids);
    }
} 
internal delegate void DAction(IActionPerformer actionPerformer, HashSet<EntityId>  ids);
