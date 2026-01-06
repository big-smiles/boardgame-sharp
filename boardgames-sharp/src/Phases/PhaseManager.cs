using boardgames_sharp.Stack;

namespace boardgames_sharp.Phases;

public interface IPhaseManager
{
    public void Pause();
    public void Resume();
}
internal sealed class PhaseManager(IPhase phase):IInitializeWithEngineRoot, IPhaseManager
{
    public void initialize(EngineRoot engineRoot)
    {
        _actionStack = engineRoot.ActionStack;
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Resume()
    {
        if (_actionStack == null)
        {
            throw new BadRootInitializationException("_actionStack is null");
        }
        
        _paused = false;
        
        if (_running)
        {
            return;
        }
        
        _running = true;
        
        while (!_paused)
        {
            phase.Next(_actionStack);    
        }
        _running = false;
    }

    private bool _paused = false;
    private bool _running = false;
    private IActionStack? _actionStack;
}