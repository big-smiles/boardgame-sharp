using boardgames_sharp.Actions.ActionPerformer;
using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;
using example_cardgame.Board;
using example_cardgame.Card;
using example_cardgame.Constants;

namespace example_cardgame.Action.Board;

public interface IBoardActionPerformer
{
    void create_board();
    IBoardTile create_board_tile(int x, int y);
    IBoardTile get_board_tile(int x, int y);
    void move_card_to_board_tile(int x, int y, EntityId cardId);
}
public class BoardActionPerformer(ICardGameActionPerformer performer):IBoardActionPerformer
{
    public void create_board()
    {
        var container = performer.Container.create_container();
        performer.BasePerformer.Entity.add_property(container.Id, CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE,
            CONSTANTS.CONTAINER_TYPES.BOARD);
        performer.BasePerformer.Entity.add_property_of_dictionary(container.Id,
            CONSTANTS.PROPERTY_IDS.IDICTIONARY.BOARD_TILES);
        performer.BasePerformer.Entity.save_entity(container.Id, CONSTANTS.KEY_ENTITY_IDS.BOARD);
    }
    
    public IBoardTile create_board_tile(int x, int y)
    {
        var pos = new Tuple<int, int>(x, y);
        var boardId = performer.BasePerformer.Entity.get_saved_entity(CONSTANTS.KEY_ENTITY_IDS.BOARD);
        var board = performer.BasePerformer.Entity.get_entity(boardId);
        var grid = board.GridOfEntityIdProperties.get_read_only(CONSTANTS.PROPERTY_IDS.IDICTIONARY.BOARD_TILES).CurrentValue();
        if (grid.ContainsKey(pos))
        {
            throw new Exception("Board tile already exists for position " + pos);
        }
        var entity = performer.Container.create_container();
        performer.BasePerformer.Entity.add_property<int>(entity.Id, CONSTANTS.PROPERTY_IDS.INT.CONTAINER_TYPE, CONSTANTS.CONTAINER_TYPES.BOARD_TILE);
        performer.BasePerformer.Entity.add_property(entity.Id, CONSTANTS.PROPERTY_IDS.INT.BOARD_TILE_X, x);
        performer.BasePerformer.Entity.add_property(entity.Id, CONSTANTS.PROPERTY_IDS.INT.BOARD_TILE_Y, y);
        performer.BasePerformer.Entity.PropertiesOfDicitonary.add_element(boardId, CONSTANTS.PROPERTY_IDS.IDICTIONARY.BOARD_TILES, pos, entity.Id);
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

