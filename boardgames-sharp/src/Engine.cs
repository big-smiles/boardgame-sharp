using boardgames_sharp.Actions;
using boardgames_sharp.GameState;
using boardgames_sharp.Interaction;

namespace boardgames_sharp;

public class Engine
{
    public Engine(IAction startingAction, HashSet<uint>? players)
    {
        Console.WriteLine("Engine initialized");
        
        players ??= new HashSet<uint>();

        var engineRoot = new EngineRoot(players);
        
        GameStateObservable = engineRoot.GameStateObservable;
        _interactionManager = engineRoot.InteractionManager;
        //TO-DO replace this with phases
        engineRoot.ActionStack.AddAction(startingAction, []);

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