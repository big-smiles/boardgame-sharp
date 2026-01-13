using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;
using example_cardgame.Constants;

namespace example_cardgame.Action.Container;


public partial class ContainerActionPerformer:IContainerActionPerformer
{
    public IEntityReadOnly create_container()
    {
        var entity = performer.BasePerformer.Entity.create_entity();
        var entityId = entity.Id;
        
        performer.BasePerformer.Entity.add_property(entityId, CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE);
        var modifier = new ModifierSetValue<int>(CONSTANTS.ENTITY_TYPES.CONTAINER);
        performer.BasePerformer.Entity.add_modifier(entityId, CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE, modifier);
        
        performer.BasePerformer.Entity.add_property_of_set(entityId, CONSTANTS.PROPERTY_IDS.ISET.ENTITY_ID.CONTAINER_CHILDREN);
        
        return entity;
    }
    
    
}