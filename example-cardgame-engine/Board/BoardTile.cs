using boardgames_sharp.Entity;
using example_cardgame.Constants;

namespace example_cardgame.Board;

public interface IBoardTile
{
    int X { get; }
    int Y { get; }
    EntityId? Card { get; }
    EntityId Id { get; }
}
internal class BoardTile:IBoardTile
{
    public BoardTile(IEntityReadOnly entity)
    {
        Id = entity.Id;
        var intProperties = entity.get_readonly_properties_of_type<int>();
        X = intProperties.get_read_only(CONSTANTS.PROPERTY_IDS.INT.BOARD_TILE_X).CurrentValue();
        Y = intProperties.get_read_only(CONSTANTS.PROPERTY_IDS.INT.BOARD_TILE_Y).CurrentValue();
        var setOfEntityIdProperties = entity.get_readonly_properties_of_set<EntityId>();
        var children = setOfEntityIdProperties.get_read_only(CONSTANTS.PROPERTY_IDS.ISET.ENTITY_ID.CONTAINER_CHILDREN).CurrentValue();
        if (children.Count <= 0)
        {
            Card = null;
        }
        else
        {
            Card = children.First();
        }
        
        
    }

    public BoardTile(StateEntity stateEntity)
    {
        Id = stateEntity.EntityId;
        X = stateEntity.IntProperties.Get(CONSTANTS.PROPERTY_IDS.INT.BOARD_TILE_X);
        Y = stateEntity.IntProperties.Get(CONSTANTS.PROPERTY_IDS.INT.BOARD_TILE_Y);
        var children = stateEntity.SetsOfEntityId.Get(CONSTANTS.PROPERTY_IDS.ISET.ENTITY_ID.CONTAINER_CHILDREN);
        if (children.Count <= 0)
        {
            Card = null;
        }
        else
        {
            Card = children.First();
        }
}

    public int X { get; }
    public int Y { get; }
    public EntityId? Card { get; }
    public EntityId Id { get; }
}