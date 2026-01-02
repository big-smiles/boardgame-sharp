namespace boardgames_sharp;

public class EngineRoot
{
    internal GameStateObservable GameStateObservable { get; } = new();

    public EngineRoot()
    {
        
    }
}

public interface IInitializeWithEngineRoot
{
    void  Initialize(EngineRoot engineRoot);
}