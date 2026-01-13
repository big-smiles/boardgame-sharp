using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;
using boardgames_sharp.Interaction;
using boardgames_sharp.Phases;
using boardgames_sharp.Player;
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
        public void Do(IActionPerformer actionPerformer, ActionContext context)
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
        
        public void Undo(IActionPerformer actionPerformer, ActionContext context)
        {
            throw new NotImplementedException();
        }
        private class ActionAfterInteraction(uint playerId) : IAction
        {
            public void Do(IActionPerformer performer, ActionContext context)
            {
                
                if (context.Targets == null || context.Targets.Count != 1)
                {
                    throw new Exception("there should be only 1 entityId selected");
                    
                }
                performer.Interaction.clear_available_interactions();
                var entity = context.Targets.First();
                var value = playerId == Constants.ValuePlayer1?Constants.ValuePlayer1:Constants.ValuePlayer2;
                var modifier = new ModifierSetValue<int>(value);
                performer.Entity.add_modifier(entity, Constants.ValueIntPropertyId, modifier);
                var result = _verifyWin(performer);
                if (result != Constants.ValueEmpty)
                {
                    var winner = new PlayerId((ulong)result);
                    performer.Player.Win(winner);
                }
                performer.GameState.PublishNew();
                
            }

            public void Undo(IActionPerformer actionPerformer, ActionContext context)
            {
                throw new NotImplementedException();
            }

            private int _verifyWin(IActionPerformer actionPerformer)
            {
                var allCells = actionPerformer.Entity.query_entity_ids(_queryEmptyCells);
                int[,] cells = new int[3,3];
                foreach (var cell in allCells)
                {
                    var entity = actionPerformer.Entity.get_entity(cell);
                    var intProperties = entity.get_readonly_properties_of_type<int>();
                    var x =  intProperties.get_read_only(Constants.XIntPropertyId).CurrentValue() - 1;
                    var y =  intProperties.get_read_only(Constants.YIntPropertyId).CurrentValue() - 1;
                    var value = intProperties.get_read_only(Constants.ValueIntPropertyId).CurrentValue();
                    cells[x,y] = value;
                }
                if (cells[1, 1] !=Constants.ValueEmpty && (
                        (cells[1, 1] == cells[1, 0] && cells[1, 1] == cells[1, 2]) || //column 1
                        (cells[1, 1] == cells[0, 1] && cells[1,1] == cells[2, 1])  || //row 1
                        (cells[1, 1] == cells[0, 0] && cells[1, 1] == cells[2, 2]) ||// main diagonal
                        (cells[1, 1] == cells[0, 2] && cells[1, 1] == cells[2, 0])   // secondary diagonal
                    ))
                {
                    return cells[1, 1];
                }
                
                if (cells[0, 0] !=Constants.ValueEmpty && (
                    (cells[0, 0] == cells[0, 1] && cells[0, 0] == cells[0, 2]) || //column 0
                    (cells[0, 0] == cells[1, 0] && cells[0,0] == cells[2, 0])   //row 0
                    ))
                {
                    return cells[0, 0];
                }
                if (cells[2, 2] !=Constants.ValueEmpty && (
                        (cells[2, 2] == cells[2, 1] && cells[2, 2] == cells[2, 0]) || //column 2
                        (cells[2, 2] == cells[1, 2] && cells[2,2] == cells[0, 2])   //row 2
                    ))
                {
                    return cells[2, 2];
                }

                return Constants.ValueEmpty;
            }
             
            private readonly QueryAllCells _queryEmptyCells = new QueryAllCells();
            private class QueryAllCells : IEntityQuery
            {
                public bool select_entity(IEntityReadOnly entity)
                {
                    var intProperties = entity.get_readonly_properties_of_type<int>();
                    return intProperties.Contains(Constants.ValueIntPropertyId);
                }
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
