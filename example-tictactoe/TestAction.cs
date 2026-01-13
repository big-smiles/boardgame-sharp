using System.Collections.Generic;
using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.Stack;

namespace boardgames_sharp.Tests;


internal class Action(DAction @do, DAction undo) : IAction
{
    public void Do(IActionPerformer actionPerformer, ActionContext context)
    {
        @do(actionPerformer, context);
    }

    public void Undo(IActionPerformer actionPerformer, ActionContext context)
    {
        undo(actionPerformer, context);
    }
} 
internal delegate void DAction(IActionPerformer actionPerformer, ActionContext context);
