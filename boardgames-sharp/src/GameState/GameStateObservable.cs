namespace boardgames_sharp.GameState;


internal sealed class GameStateObservable: IObservable<IGameState>, IInitializeWithEngineRoot
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
        //Here we are hedging against the observer calling an Interaction during the OnNext
        //and that interaction possibly producing a new Publish
        if(!_observers.Contains(observer))
        {
            var index = 0;
            while( index < _gameStates.Count) 
            {
                var gameState = _gameStates[index];
                observer.OnNext(gameState);
                index++;
            }
        }
        _observers.Add(observer);
        return new Unsubscriber<IGameState>(_observers, observer);
    }
    
    private readonly HashSet<IObserver<IGameState>> _observers = new();
    private readonly List<IGameState> _gameStates = new();
    public void initialize(EngineRoot engineRoot)
    {
    }
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