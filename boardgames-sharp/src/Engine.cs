using boardgames_sharp.Actions;
using boardgames_sharp.GameState;

namespace boardgames_sharp;

public class Engine
{
    public Engine(IAction startingAction)
    {
        Console.WriteLine("Engine initialized");
        var engineRoot = new EngineRoot();
        GameStateObservable = engineRoot.GameStateObservable;
        engineRoot.ActionStack.AddAction(startingAction);

    }
    public IObservable<IGameState> GameStateObservable { get; }
}