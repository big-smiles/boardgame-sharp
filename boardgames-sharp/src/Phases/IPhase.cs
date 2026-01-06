using boardgames_sharp.Actions;
using boardgames_sharp.Stack;

namespace boardgames_sharp.Phases;

public interface IPhase
{
    public bool Next(IActionStack actionStack);
}

public class PhaseGroup(List<IPhase> phases, bool loops): IPhase
{
    public bool Next(IActionStack actionStack)
    {
        if (phases.Count == 0)
        {
            throw new EmptyPhaseGroupException();
        }
        
        var ret = phases[_index].Next(actionStack);
        if (ret) return true;
        _index++;
        if (_index >= phases.Count)
        {
            if (loops)
            {
                _index = 0;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    private int _index = 0;
}

public class EmptyPhaseGroupException : Exception
{
    
}

public class Phase(IAction action): IPhase
{
    public bool Next(IActionStack actionStack)
    {
        actionStack.AddPhaseAction(action);
        return false;
    }
}