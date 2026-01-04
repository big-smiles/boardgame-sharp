using System;
using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.GameState;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace boardgames_sharp.Tests;

[TestClass]
[TestSubject(typeof(Engine))]
public class EngineTest
{
    [TestMethod]
    public void CreateEmptyState()
    {
        var startingAction = 
            new TestAction(
                @do: (performer => performer.GameState.PublishNew()),
            undo: (performer => Assert.Fail("this shouldn't be called here")));
        var engine = new Engine(startingAction);
        var count = 0;
        var dispose = engine.GameStateObservable.Subscribe(
            new TestObserver(
                onComplete: () => Assert.Fail("this shouldn't be called here"),
                onError: (error) => Assert.Fail("this shouldn't be called here"),
                onNext: (state) =>
                {
                    count++;
                    Assert.IsEmpty(state.Entities.Entities,"The state should have 0 entities");
                }
            )
        );
        Assert.AreEqual(1, count, "should return one initial state");
        
    }
    [TestMethod]
    public void CreateStateWithSingleEntity()
    {
        var startingAction = 
            new TestAction(
                @do: (performer =>
                {
                    var entity = performer.Entity.CreateEntity();
                    Assert.IsNotNull(entity);
                    performer.GameState.PublishNew();
                }),
                undo: (performer => Assert.Fail("this shouldn't be called here")));
        var engine = new Engine(startingAction);
        var count = 0;
        var dispose = engine.GameStateObservable.Subscribe(
            new TestObserver(
                onComplete: () => Assert.Fail("this shouldn't be called here"),
                onError: (error) => Assert.Fail("this shouldn't be called here"),
                onNext: (state) =>
                {
                    count++;
                    Assert.HasCount(1, state.Entities.Entities, "The state should have 1 entities");
                }
            )
        );
        Assert.AreEqual(1, count, "should return one initial state");
        
    }
    
}

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

internal class TestAction(DAction @do, DAction undo) : IAction
{
    public void Do(IActionPerformer actionPerformer)
    {
        @do(actionPerformer);
    }

    public void Undo(IActionPerformer actionPerformer)
    {
        undo(actionPerformer);
    }
} 
internal delegate void DAction(IActionPerformer actionPerformer);