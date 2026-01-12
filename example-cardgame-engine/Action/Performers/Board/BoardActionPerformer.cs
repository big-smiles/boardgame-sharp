using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;
using example_cardgame.Board;
using example_cardgame.Card;
using example_cardgame.Constants;

namespace example_cardgame.Action.Board;

public interface IBoardActionPerformer
{
    IBoardTile create_board_tile(int x, int y);
    IBoardTile get_board_tile(int x, int y);
    void move_card_to_board_tile(int x, int y, EntityId cardId);
}
public class BoardActionPerformer(IActionPerformer basePerformer, ICardGameActionPerformer performer):IBoardActionPerformer
{
    public IBoardTile create_board_tile(int x, int y)
    {
        var entity = performer.Container.create_container();
        performer.BasePerformer.Entity.add_property<int>(entity.Id, CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE, CONSTANTS.CONTAINER_TYPES.BOARD_TILE);
        performer.BasePerformer.Entity.add_property(entity.Id, CONSTANTS.PROPERTY_IDS.INT.BOARD_TILE_X, x);
        performer.BasePerformer.Entity.add_property(entity.Id, CONSTANTS.PROPERTY_IDS.INT.BOARD_TILE_Y, y);
        var boardTile = new BoardTile(entity);
        return boardTile;
    }

    public IBoardTile get_board_tile(int x, int y)
    {
        var entityIds = performer.BasePerformer.Entity.query_entity_ids(_get_query_board_tile(x,y));
        if (entityIds == null || entityIds.Count == 0)
        {
            throw new Exception("No board tile found for x= "+ x + " , y= " + y);
        }

        if (entityIds.Count > 1)
        {
            throw new Exception("There shouldnt be 2 tiles with same coordinates");
        }
        var entity = performer.BasePerformer.Entity.get_entity(entityIds.First());
        var boardTile = new BoardTile(entity);
        return boardTile;
    }

    public void move_card_to_board_tile(int x, int y, EntityId cardId)
    {
        var boardTile = get_board_tile(x, y);
        if (boardTile.Card != null)
        {
            throw new Exception("Tile was not empty");
        }
        performer.Container.add_entity_to_container(cardId, boardTile.Id);
        performer.Card.set_card_location(cardId, ECardLocations.Board);
    }

    private IEntityQuery _get_query_board_tile(int x, int y)
    {
        return new EntityQuery(entity =>
        {
            var foundContainerType =
                entity.try_and_get_value_of_type<int>(CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE, out var containerType);
            if (!foundContainerType || containerType != CONSTANTS.CONTAINER_TYPES.BOARD_TILE)
            {
                return false;
            }

            var foundX = entity.try_and_get_value_of_type(CONSTANTS.PROPERTY_IDS.INT.BOARD_TILE_X, out var valueX);
            if (!foundX || valueX != x)
            {
                return false;
            }

            var foundY = entity.try_and_get_value_of_type(CONSTANTS.PROPERTY_IDS.INT.BOARD_TILE_Y, out var valueY);
            if (!foundY || valueY != y)
            {
                return false;
            }

            return true;
        });
    }
    
}

