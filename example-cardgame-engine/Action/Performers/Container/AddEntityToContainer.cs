using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;
using example_cardgame.Constants;

namespace example_cardgame.Action.Container;


public partial class ContainerActionPerformer:IContainerActionPerformer
{
    public void add_entity_to_container(EntityId entityId, EntityId containerId)
    {
        var container = performer.BasePerformer.Entity.get_entity(containerId);
        if (!_checkEntityIsContainer(container, out var error))
        {
            throw new InvalidEntityType(error);
        }
        var entity = performer.BasePerformer.Entity.get_entity(entityId);
        _remove_entity_from_parent(entity);

        var entityIdProperties = entity.get_readonly_properties_of_type<EntityId>();
        //if this entity was never added to a contianer before we
        //might need to add the property to track parents
        if (!entityIdProperties.Contains(CONSTANTS.PROPERTY_IDS.ENTITY_IDS.CONTAINER_PARENT))
        {
            performer.BasePerformer.Entity.add_property(
                entityId,
                CONSTANTS.PROPERTY_IDS.ENTITY_IDS.CONTAINER_PARENT
            );
        }
        //we set the parent on entityId
        var modifierContainerParent = new ModifierSetValue<EntityId>(containerId);
        performer.BasePerformer.Entity.add_modifier(
            entityId,
            CONSTANTS.PROPERTY_IDS.ENTITY_IDS.CONTAINER_PARENT,
            modifierContainerParent
        );
        //we add entityId as childer on the container
        performer.BasePerformer.Entity.PropertiesOfSet.add_element<EntityId>(
            containerId, 
            CONSTANTS.PROPERTY_IDS.ISET.ENTITY_ID.CONTAINER_CHILDREN,
            entityId
        );
        
    }

    private void _remove_entity_from_parent(IEntityReadOnly entity)
    {
        var entityIdProperties = entity.get_readonly_properties_of_type<EntityId>();
        if (!entityIdProperties.Contains(CONSTANTS.PROPERTY_IDS.ENTITY_IDS.CONTAINER_PARENT))
        {
            return;
        }
        var parentId = entityIdProperties.get_read_only(CONSTANTS.PROPERTY_IDS.ENTITY_IDS.CONTAINER_PARENT);
          
    }
}