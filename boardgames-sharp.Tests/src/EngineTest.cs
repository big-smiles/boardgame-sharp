using System;
using boardgames_sharp;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace boardgames_sharp.Tests;

[TestClass]
[TestSubject(typeof(Engine))]
public class EngineTest
{
    [TestMethod]
    public void Method()
    {
        var engine = new Engine();
        var count = 0;
        var dispose = engine.GameStateObservable.Subscribe(
            new TestObserver(
                onComplete: () => Assert.Fail(),
                onError: (error) => Assert.Fail(),
                onNext: (state) => count++
            )
        );
        Assert.AreEqual(1, count, "should return one initial state");
    
    }
    
}

internal class TestObserver : IObserver<IGameState>
{
    private readonly DOnComplete _onComplete;
    private readonly DOnError _onError;
    private readonly DOnNext _onNext;

    internal TestObserver(DOnComplete onComplete, DOnError onError, DOnNext onNext)
    {
        _onComplete = onComplete;
        _onError = onError;
        _onNext = onNext;
    }
    public void OnCompleted()
    {
        this._onComplete();
    }

    public void OnError(Exception error)
    {
        this._onError(error);
    }

    public void OnNext(IGameState value)
    {
        this._onNext(value);
    }
}

internal delegate void DOnComplete(); 
internal delegate void DOnError(Exception error); 
internal delegate void DOnNext(IGameState value); 