using System;
using boardgames_sharp.GameState;

namespace boardgames_sharp.Tests;


internal class TestObserver(DOnComplete onComplete, DOnError onError, DOnNext onNext) : IObserver<IGameState>
{
    public void OnCompleted()
    {
        onComplete();
    }

    public void OnError(Exception error)
    {
        onError(error);
    }

    public void OnNext(IGameState value)
    {
        onNext(value);
    }
}

internal delegate void DOnComplete(); 
internal delegate void DOnError(Exception error); 
internal delegate void DOnNext(IGameState value);
