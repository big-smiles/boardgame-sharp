using boardgames_sharp.Entity;

namespace example_cardgame.Card;

public readonly record struct CardId
{
    public CardId(EntityId entityId)
    {
        Id = entityId.Id;
        EntityId = entityId;
    }
    public ulong Id { get;}
    public EntityId EntityId { get;}

}