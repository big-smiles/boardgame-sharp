using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.Player;

namespace boardgames_sharp.Stack;

public interface IActionStack
{
    public void AddPhaseAction(IAction action);
    public void AddInteractionAction(IAction action, IReadOnlySet<EntityId> selectedEntityIds);
    public void AddActionFromPerformer(IAction action,PlayerId? owner, EntityId? source, IReadOnlySet<EntityId> targets);
    
}
internal sealed class ActionStack: IInitializeWithEngineRoot, IActionStack
{
    public void initialize(EngineRoot engineRoot)
    {
        this._actionPerformer = engineRoot.ActionPerformer;
        this._root = engineRoot;
    }

    public void AddPhaseAction(IAction action)
    {
        var context = new ActionContext(null, null, null);
        AddAction(action, context);
    }

    public void AddInteractionAction(IAction action, IReadOnlySet<EntityId> selectedEntityIds)
    {
        var context = new ActionContext(selectedEntityIds, null, null);
        AddAction(action, context);
    }

    public void AddActionFromPerformer(IAction action, PlayerId? owner, EntityId? source, IReadOnlySet<EntityId> targets)
    {
        var context = new ActionContext(
            targets:targets,
            owner:owner,
            source:source
        );
        AddAction(action, context);
    }

    private void AddAction(IAction action, ActionContext context)
    {
        _stack.Push(new Tuple<IAction, ActionContext>(action, context));
        if (_isRunning) return;
        _isRunning = true;
        _run();
        _isRunning = false;
    }
    private void _run()
    {
        if (this._actionPerformer == null)
        {
            throw new NullReferenceException("ActionPerformer is null");
        }
        while (true)
        {
            if (_stack.Count == 0)
            {
                return;
            }

            var tuple = _stack.Pop();
            var action = tuple.Item1;
            var entities = tuple.Item2;
            action.Do(this._actionPerformer, entities);
            
        }
    }
    private IActionPerformer? _actionPerformer;
    private readonly Stack<Tuple<IAction,ActionContext>> _stack = new();
    private bool _isRunning = false;
    private EngineRoot? _root;
}

public readonly record struct ActionContext
{
    public readonly IReadOnlySet<EntityId>? Targets;
    public readonly EntityId? Source;
    public readonly PlayerId? Owner;
    public ActionContext( IReadOnlySet<EntityId>? targets, PlayerId? owner, EntityId? source)
    {
        Owner = owner;
        Source = source;
        Targets = targets;
    }
}  