using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using example_cardgame.Constants;

namespace example_cardgame.Action.Container;

public interface IContainerActionPerformer
{
    IEntityReadOnly create_container();
    void add_entity_to_container(EntityId entityId, EntityId containerId);
}
public partial class ContainerActionPerformer(IActionPerformer baseActionPerformer):IContainerActionPerformer
{
    private bool _checkEntityIsContainer(IEntityReadOnly entity, out string message)
    {
        var intProperties = entity.get_readonly_properties_of_type<int>();
        if (!intProperties.Contains(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE))
        {
            message = "container did not have property for ENTITY_TYPE";
            return false;
        }

        if (intProperties.get_read_only(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE).CurrentValue() != CONSTANTS.ENTITY_TYPES.CONTAINER)
        {
            message = "container ENTITY_TYPE had incorrect value=" 
                      + intProperties.get_read_only(CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE).CurrentValue() 
                      + " expected was=" +  CONSTANTS.ENTITY_TYPES.CONTAINER;
            return false;
        }

        var setOfEntityIdProperties = entity.get_readonly_properties_of_set_of_type<EntityId>();
        if (!setOfEntityIdProperties.Contains(CONSTANTS.PROPERTY_IDS.ISET.ENTITY_ID.CONTAINER_CHILDREN))
        {
            message = "container did not have property for CONTAINER_CHILDREN";
            return false;
        }
        message = "";
        return true;
    }
    
}