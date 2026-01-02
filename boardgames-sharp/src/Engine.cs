namespace boardgames_sharp;

public class Engine
{
    public Engine()
    {
        Console.WriteLine("Engine initialized");
        var engineRoot = new EngineRoot();
        GameStateObservable = engineRoot.GameStateObservable;
    }
    public IObservable<IGameState> GameStateObservable { get; }
}