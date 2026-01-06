using boardgames_sharp.Phases;

namespace boardgames_sharp.Actions.ActionPerformer.Phase;

public interface IPhaseActionPerformer
{
    void Pause();
    void Resume();
}
internal sealed class PhaseActionPerformer: IInitializeWithEngineRoot, IPhaseActionPerformer
{
    public void initialize(EngineRoot engineRoot)
    {
        _phaseManager = engineRoot.PhaseManager;
    }

    public void Pause()
    {
        if (_phaseManager == null)
        {
            throw new BadRootInitializationException("_phaseManager was not initialized");
        }
        _phaseManager.Pause();
    }

    public void Resume()
    {
        if (_phaseManager == null)
        {
            throw new BadRootInitializationException("_phaseManager was not initialized");
        }
        _phaseManager.Resume();
    }
    
    private IPhaseManager? _phaseManager;
}