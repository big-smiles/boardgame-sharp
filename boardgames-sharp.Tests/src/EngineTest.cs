using System;
using System.Collections.Generic;
using System.Linq;
using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;
using boardgames_sharp.GameState;
using boardgames_sharp.Phases;
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
                @do: ((performer, ids) =>
                {
                    performer.Phase.Pause();
                    performer.GameState.PublishNew();
                }),
            undo: ((performer, ids) => Assert.Fail("this shouldn't be called here")));
        var phase = new Phase(startingAction);
        var engine = new Engine(null,phase);
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
        engine.Start();
        Assert.AreEqual(1, count, "should return one initial state");
        
    }
    [TestMethod]
    public void CreateStateWithSingleEntity()
    {
        var startingAction = 
            new TestAction(
                @do: ((performer, ids) =>
                {
                    var entity = performer.Entity.create_entity();
                    Assert.IsNotNull(entity);
                    performer.Phase.Pause();
                    performer.GameState.PublishNew();
                }),
                undo: ((performer, ids) => Assert.Fail("this shouldn't be called here")));
        var phase = new Phase(startingAction);
        var engine = new Engine(null, phase);
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
        engine.Start();
        Assert.AreEqual(1, count, "should return one initial state");
        
    }
    [TestMethod]
    public void CreateStateWithSingleEntityWithASetValue()
    {
        ulong propertyIdValue = 999;
        int propertyValue = 12;
        var startingAction = 
            new TestAction(
                @do: ((performer, ids) =>
                {
                    var entity = performer.Entity.create_entity();
                    Assert.IsNotNull(entity);
                    var intProperties = entity.get_readonly_properties_of_type<int>();
                    Assert.IsNotNull(intProperties, "should have int properties");
                    var propertyId = new PropertyId<int>(propertyIdValue);
                    var property = performer.Entity.add_property<int>(entity.Id, propertyId);
                    var modifier = new ModifierSetValue<int>(propertyValue);
                    performer.Entity.add_modifier(entity.Id,propertyId, modifier);
                    performer.Phase.Pause();
                    performer.GameState.PublishNew();
                    
                }),
                undo: ((performer, ids) => Assert.Fail("this shouldn't be called here")));
        var phase = new Phase(startingAction);
        var engine = new Engine(null, phase);
        var count = 0;
        var dispose = engine.GameStateObservable.Subscribe(
            new TestObserver(
                onComplete: () => Assert.Fail("this shouldn't be called here"),
                onError: (error) => Assert.Fail("this shouldn't be called here"),
                onNext: (state) =>
                {
                    count++;
                    Assert.HasCount(1, state.Entities.Entities, "The state should have 1 entities");
                    var entity = state.Entities.Entities[0];
                    Assert.IsNotNull(entity);
                    var intProperties = entity.Item2.IntProperties;
                    Assert.IsNotNull(intProperties, "should have int properties");
                    Assert.HasCount(1,intProperties.Properties, "Should have 1 property");
                    var propertyId = intProperties.Properties[0].Item1;
                    Assert.AreEqual(propertyIdValue, propertyId.Id);
                    Assert.AreEqual(propertyValue, intProperties.Properties[0].Item2);
                }
            )
        );
        engine.Start();
        Assert.AreEqual(1, count, "should return one initial state");
        
    }   
    [TestMethod]
    public void AfterInteractionModifyEntity()
    {
        ulong propertyIdValue = 999;
        int propertyValue = 12;
        var startingAction = 
            new TestAction(
                @do: ((performer, ids) =>
                {
                    var entity = performer.Entity.create_entity();
                    Assert.IsNotNull(entity);
                    var intProperties = entity.get_readonly_properties_of_type<int>();
                    Assert.IsNotNull(intProperties, "should have int properties");
                    var propertyId = new PropertyId<int>(propertyIdValue);
                    var intProperty = performer.Entity.add_property<int>(entity.Id, propertyId);
                    var entityIds = new HashSet<EntityId>(){entity.Id};
                    performer.Interaction.add_available_interaction(
                        playerId: 1,
                        availableEntityIds:entityIds,
                        min:1,
                        max:2, 
                        actionWhenSelected:new TestAction(
                            @do: (actionPerformer, ids) =>
                            {
                                Assert.HasCount(1, ids);
                                var entityId = ids.First();
                                var intPropertyId = new PropertyId<int>(propertyIdValue);
                                var modifier = new ModifierSetValue<int>(propertyValue);
                                performer.Entity.add_modifier(entityId, intPropertyId, modifier);
                                performer.Interaction.clear_available_interactions();
                                // performer.Phase.Pause();
                                performer.GameState.PublishNew();
                                    
                            }, 
                            @undo: (actionPerformer, set) =>
                            {
                                Assert.Fail("should not undo");
                            }
                        )
                    );
                    performer.Phase.Pause();
                    performer.GameState.PublishNew();
   
                }),
                undo: ((performer, ids) => Assert.Fail("this shouldn't be called here")));
        var phase = new Phase(startingAction);
        var engine = new Engine(null, phase);
        var count = 0;
        var dispose = engine.GameStateObservable.Subscribe(
            new TestObserver(
                onComplete: () => Assert.Fail("this shouldn't be called here"),
                onError: (error) => Assert.Fail("this shouldn't be called here"),
                onNext: (state) =>
                {
                    Console.WriteLine(state.Id);
                    count++;
                    if (count == 1)
                    {
                        Assert.HasCount(1, state.Entities.Entities, "The state should have 1 entities");
                        var tuple = state.Entities.Entities[0];
                        Assert.IsNotNull(tuple);
                        var entityId = tuple.Item1;
                        var intProperties = tuple.Item2.IntProperties;
                        Assert.IsNotNull(intProperties, "should have int properties");
                        Assert.HasCount(1, intProperties.Properties, "Should have 1 property");
                        var propertyId = intProperties.Properties[0].Item1;
                        Assert.AreEqual(propertyIdValue, propertyId.Id);
                        Assert.AreEqual(0, intProperties.Properties[0].Item2, "the property should be on zerovalue");
                        Assert.HasCount(1, state.AvailableInteractions, "Should have 1 available interactions");
                        var availableInteraction = state.AvailableInteractions[0];
                        var interaction = new Interaction.Interaction(availableInteraction.InteractionId, new HashSet<EntityId>(){entityId});
                        engine.Interact(interaction);
                    }
                    else if (count == 2)
                    {
                        Assert.HasCount(1, state.Entities.Entities, "The state should have 1 entities");
                        var entity = state.Entities.Entities[0];
                        Assert.IsNotNull(entity);
                        var intProperties = entity.Item2.IntProperties;
                        Assert.IsNotNull(intProperties, "should have int properties");
                        Assert.HasCount(1, intProperties.Properties, "Should have 1 property");
                        var propertyId = intProperties.Properties[0].Item1;
                        Assert.AreEqual(propertyIdValue, propertyId.Id);
                        Assert.AreEqual(propertyValue, intProperties.Properties[0].Item2,"the property should have its value modified");
                        Assert.HasCount(0, state.AvailableInteractions, "Should have 0 available interactions");
                    }
                }
            )
        );
        engine.Start();
        Assert.AreEqual(2, count, "should return two states");
        
    }
}
