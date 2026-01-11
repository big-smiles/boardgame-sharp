using example_cardgame;
internal interface IPublishCardGameState
{
    void Publish(ICardGameState gameState);
}
internal sealed class CardGameStateObservable: IObservable<ICardGameState>, IPublishCardGameState
{
    public CardGameStateObservable()
    {}

    public void Publish(ICardGameState gameState)
    {
        this._gameStates.Add(gameState);
        foreach (var observer in _observers)
        {
            observer.OnNext(gameState);
        }
    }
    public IDisposable Subscribe(IObserver<ICardGameState> observer)
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
        return new Unsubscriber<ICardGameState>(_observers, observer);
    }
    
    private readonly HashSet<IObserver<ICardGameState>> _observers = new();
    private readonly List<ICardGameState> _gameStates = new();
  
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