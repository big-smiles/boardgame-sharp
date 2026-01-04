using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;

namespace boardgames_sharp.Stack;

public class ActionStack: IInitializeWithEngineRoot
{
    public void Initialize(EngineRoot engineRoot)
    {
        this._actionPerformer = engineRoot.ActionPerformer;
        this._root = engineRoot;
    }

    public void AddAction(IAction action)
    {
        _stack.Push(action);
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

            var action = _stack.Pop();
            action.Do(this._actionPerformer);
            
        }
    }
    private IActionPerformer? _actionPerformer;
    private readonly Stack<IAction> _stack = new();
    private bool _isRunning = false;
    private EngineRoot? _root;
}