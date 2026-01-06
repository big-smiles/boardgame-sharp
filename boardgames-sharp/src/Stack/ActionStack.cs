using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;

namespace boardgames_sharp.Stack;

public interface IActionStack
{
    public void AddPhaseAction(IAction action);
    public void AddInteractionAction(IAction action, HashSet<EntityId> entities);
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
        AddInteractionAction(action, []);
    }

    public void AddInteractionAction(IAction action, HashSet<EntityId> entities)
    {
        _stack.Push(new Tuple<IAction, HashSet<EntityId>>(action, entities));
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
    private readonly Stack<Tuple<IAction,HashSet<EntityId>>> _stack = new();
    private bool _isRunning = false;
    private EngineRoot? _root;
}