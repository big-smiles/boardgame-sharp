using System.Collections.Generic;
using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.Stack;

namespace boardgames_sharp.Tests;


internal class TestAction(DAction @do, DAction undo) : IAction
{
    public void Do(IActionPerformer actionPerformer, ActionContext actionContext)
    {
        @do(actionPerformer, actionContext);
    }

    public void Undo(IActionPerformer actionPerformer, ActionContext actionContext)
    {
        undo(actionPerformer, actionContext);
    }
} 
internal delegate void DAction(IActionPerformer actionPerformer, ActionContext actionContext);
