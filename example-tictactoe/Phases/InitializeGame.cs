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
        public void Do(IActionPerformer actionPerformer, ActionContext context)
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

        public void Undo(IActionPerformer actionPerformer, ActionContext context)
        {
            throw new NotImplementedException();
        }
        private void CreateEntity(IActionPerformer performer, int x, int y)
        {
            var entity = performer.Entity.create_entity();
            
            performer.Entity.add_property(entity.Id, Constants.XIntPropertyId);
            performer.Entity.add_modifier(entity.Id, Constants.XIntPropertyId, new ModifierSetValue<int>(x));
            performer.Entity.add_property(entity.Id, Constants.YIntPropertyId);
            performer.Entity.add_modifier(entity.Id, Constants.YIntPropertyId, new ModifierSetValue<int>(y));
            performer.Entity.add_property(entity.Id, Constants.ValueIntPropertyId);
            performer.Entity.add_modifier(entity.Id, Constants.ValueIntPropertyId, new ModifierSetValue<int>(Constants.ValueEmpty));
           
        }
    }
}