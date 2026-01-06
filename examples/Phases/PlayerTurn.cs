using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;
using boardgames_sharp.Interaction;
using boardgames_sharp.Phases;
using boardgames_sharp.Stack;

namespace examples.Phases;

public class PlayerTurnPhase(uint playerId):IPhase
{
    public bool Next(IActionStack actionStack)
    {
        var action = new Action(playerId);
        actionStack.AddPhaseAction(action);
        return false;
    }
    
    private class Action(uint playerId): IAction
    {
        public void Do(IActionPerformer actionPerformer, HashSet<EntityId> entityIds)
        {
            var entities = actionPerformer.Entity.query_entity_ids(_queryEmptyCells);
            var action = new ActionAfterInteraction(playerId);
            actionPerformer.Interaction.add_available_interaction(
                playerId:playerId,
                availableEntityIds:entities,
                min: 1,
                max: 2,
                action
                );
            actionPerformer.GameState.PublishNew();
        }
        
        public void Undo(IActionPerformer actionPerformer, HashSet<EntityId> entityIds)
        {
            throw new NotImplementedException();
        }
        private class ActionAfterInteraction(uint playerId) : IAction
        {
            public void Do(IActionPerformer performer, HashSet<EntityId> entityIds)
            {
                if (entityIds.Count != 1)
                {
                    throw new Exception("there should be only 1 entityId selected");
                    
                }
                performer.Interaction.clear_available_interactions();
                var entity = entityIds.First();
                var value = playerId == Constants.Player1?Constants.ValuePlayer1:Constants.ValuePlayer2;
                var modifier = new ModifierSetValue<int>(value);
                performer.Entity.add_modifier(entity, Constants.ValueIntPropertyId, modifier);
                performer.GameState.PublishNew();
                
            }

            public void Undo(IActionPerformer actionPerformer, HashSet<EntityId> entityIds)
            {
                throw new NotImplementedException();
            }
        }
        private readonly IEntityQuery _queryEmptyCells = new QueryEmptyCells();
        
        private class QueryEmptyCells: IEntityQuery
        {
            public bool select_entity(IEntityReadOnly entity)
            {
                var intProperties = entity.get_readonly_properties_of_type<int>();
                if (!intProperties.Contains(Constants.ValueIntPropertyId))
                {
                    return false;
                }
                var valueProperty = intProperties.get_read_only(Constants.ValueIntPropertyId);
                return valueProperty.CurrentValue() == Constants.ValueEmpty;
            }
        }
    }
}
