namespace boardgames_sharp;

public interface IGameState
{
    
}

internal sealed class GameStateObservable: IObservable<IGameState>
{
    public GameStateObservable()
    {}

    public void Publish(IGameState gameState)
    {
        this._gameStates.Add(gameState);
        foreach (var observer in _observers)
        {
            observer.OnNext(gameState);
        }
    }
    public IDisposable Subscribe(IObserver<IGameState> observer)
    {
        if(_observers.Add(observer))
        {
            foreach (var gameState in _gameStates)
            {
                observer.OnNext(gameState);
            }
        }
        return new Unsubscriber<IGameState>(_observers, observer);
    }
    
    private readonly HashSet<IObserver<IGameState>> _observers = new();
    private List<IGameState> _gameStates = new();
}

internal sealed class Unsubscriber<T> : IDisposable
{
    
    private readonly ISet<IObserver<T>> _observers;
    private readonly IObserver<T> _observer;

    internal Unsubscriber(
        ISet<IObserver<T>> observers,
        IObserver<T> observer) => (_observers, _observer) = (observers, observer);

    public void Dispose() => _observers.Remove(_observer);
}