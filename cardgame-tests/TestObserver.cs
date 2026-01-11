using example_cardgame;

namespace cardgame_tests;

internal class TestObserver(DOnComplete onComplete, DOnError onError, DOnNext onNext) : IObserver<ICardGameState>
{
    public void OnCompleted()
    {
        onComplete();
    }

    public void OnError(Exception error)
    {
        onError(error);
    }

    public void OnNext(ICardGameState value)
    {
        onNext(value);
    }
}

internal delegate void DOnComplete(); 
internal delegate void DOnError(Exception error); 
internal delegate void DOnNext(ICardGameState value);