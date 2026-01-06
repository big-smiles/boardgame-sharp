using boardgames_sharp.Actions;
using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;
using boardgames_sharp.Phases;
using boardgames_sharp.Stack;

namespace examples.Phases;

public class InitializeGamePhase():IPhase
{
    public bool Next(IActionStack actionStack)
    {
        var action = new Action();
        actionStack.AddPhaseAction(action);
        return false;
    }
    
    private class Action: IAction
    {
        public void Do(IActionPerformer actionPerformer, HashSet<EntityId> entityIds)
        {
            CreateEntity(actionPerformer,1,1);
            CreateEntity(actionPerformer,1,2);
            CreateEntity(actionPerformer,1,3);
            CreateEntity(actionPerformer,2,1);
            CreateEntity(actionPerformer,2,2);
            CreateEntity(actionPerformer,2,3);
            CreateEntity(actionPerformer,3,1);
            CreateEntity(actionPerformer,3,2);
            CreateEntity(actionPerformer,3,3);
        }

        public void Undo(IActionPerformer actionPerformer, HashSet<EntityId> entityIds)
        {
            throw new NotImplementedException();
        }
        private void CreateEntity(IActionPerformer performer, int x, int y)
        {
            var entity = performer.Entity.create_entity();
            var intProperties = entity.GetPropertiesOfType<int>();
            var xProperty = intProperties.Add(Constants.XIntPropertyId);
            xProperty.AddModifier(new ModifierSetValue<int>(x));
            var yProperty = intProperties.Add(Constants.YIntPropertyId);
            yProperty.AddModifier(new ModifierSetValue<int>(y));
            var valueProperty = intProperties.Add(Constants.ValueIntPropertyId);
            valueProperty.AddModifier(new ModifierSetValue<int>(Constants.ValueEmpty));
        }
    }
}