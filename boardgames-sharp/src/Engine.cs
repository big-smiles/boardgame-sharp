using boardgames_sharp.Actions;
using boardgames_sharp.GameState;
using boardgames_sharp.Interaction;
using boardgames_sharp.Phases;

namespace boardgames_sharp;

public class Engine
{
    public Engine(HashSet<uint>? players, IPhase phase)
    {
        Console.WriteLine("Engine initialized");
        
        players ??= new HashSet<uint>();

        var engineRoot = new EngineRoot(players, phase);
        
        GameStateObservable = engineRoot.GameStateObservable;
        _interactionManager = engineRoot.InteractionManager;
        //TO-DO replace this with phases
        _phaseManager = engineRoot.PhaseManager;

    }
    private IPhaseManager _phaseManager;
    public void Start()
    {
        _phaseManager.Resume();   
    }
    public IObservable<IGameState> GameStateObservable { get; }

    public void Interact(Interaction.Interaction interaction)
    {
        if (_interactionManager == null)
        {
            throw new BadRootInitializationException("interaction manager not initialized");
        }
        _interactionManager.receive_interaction(interaction);
    }
    private readonly IInteractionManager? _interactionManager;
}