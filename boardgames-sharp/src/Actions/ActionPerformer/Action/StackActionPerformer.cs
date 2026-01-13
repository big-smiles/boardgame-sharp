using boardgames_sharp.Entity;
using boardgames_sharp.Player;
using boardgames_sharp.Stack;

namespace boardgames_sharp.Actions.ActionPerformer.Action;

public interface IStackActionPerformer
{
    void add_action_to_stack(IAction action, PlayerId? owner, EntityId? source, IReadOnlySet<EntityId> targets);
}

internal class StackActionPerformer:IStackActionPerformer, IInitializeWithEngineRoot
{
    public void add_action_to_stack(IAction action, PlayerId? owner, EntityId? source, IReadOnlySet<EntityId> targets)
    {
        if (_stack == null)
        {
            throw new BadRootInitializationException("Stack not initialized");
        }
        _stack.AddActionFromPerformer(
            action:action,
            owner:owner,
            source:source,
            targets:targets
        );
    }
    
    public void initialize(EngineRoot engineRoot)
    {
        _stack = engineRoot.ActionStack;
    }

    private IActionStack? _stack;
   
}