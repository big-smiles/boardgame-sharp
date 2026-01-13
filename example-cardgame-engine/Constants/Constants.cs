using boardgames_sharp.Actions;
using boardgames_sharp.Entity;
using boardgames_sharp.Player;

namespace example_cardgame.Constants;

public static class CONSTANTS
{
    public static class PLAYER_IDS
    {
        public static readonly PlayerId PLAYER = new PlayerId(1); 
    }
    public static class KEY_ENTITY_IDS
    {
        public const int PLAYER_DECK = 1;
        public const int PLAYER_HAND = 2;
        public const int PLAYER_BOARD = 3;
        public const int PLAYER_DISCARD = 4;
        public const int PLAYER_CHARACTER = 5;
        public const int BOARD = 6;
    }
    public static class ENTITY_TYPES
    {
        public const int CONTAINER = 1;
        public const int CARD = 2;
        public const int PLAYER_CHARACTER = 3;
    }

    public static class CONTAINER_TYPES
    {
        public const int BOARD_TILE = 1;
        public const int CARD_DECK = 2;
        public const int BOARD = 3;
    }
    public static class PROPERTY_IDS
    {
        public static class INT
        {
            public static readonly PropertyId<int> ENTITY_TYPE = new PropertyId<int>(1);
            public static readonly PropertyId<int> CARD_LOCATION = new PropertyId<int>(2);
            public static readonly PropertyId<int> BOARD_TILE_X = new PropertyId<int>(3);
            public static readonly PropertyId<int> BOARD_TILE_Y = new PropertyId<int>(4);
            public static readonly PropertyId<int> CONTAINER_TYPE = new PropertyId<int>(5);
            public static readonly PropertyId<int> PLAYER_HEALTH = new PropertyId<int>(6);
            public static readonly PropertyId<int> CAN_BE_PLAYED_COOLDOWN = new PropertyId<int>(7);
        }
        public static class ENTITY_IDS
        {
            public static readonly PropertyId<EntityId> CONTAINER_PARENT = new PropertyId<EntityId>(1);    
        }

        public static class STRING
        {
            public static readonly PropertyId<string> CARD_NAME = new PropertyId<string>(1);
        }

        public static class ACTION
        {
            public static readonly PropertyId<IAction?> CARD_ONPLAY =  new PropertyId<IAction?>(1);
        }
        public static class ISET
        {
            public static class ENTITY_ID
            {
                public static readonly PropertyId<ISet<EntityId>> CONTAINER_CHILDREN = new PropertyId<ISet<EntityId>>(1);
            }
        }

        public static class BOOL
        {
            public static readonly PropertyId<bool> HAS_CAN_BE_PLAYED = new PropertyId<bool>(1);
        }

        public static class IDICTIONARY
        {
            public static readonly PropertyId<IDictionary<Tuple<int, int>, EntityId>> BOARD_TILES = new(1);
        }
    }
    
}



