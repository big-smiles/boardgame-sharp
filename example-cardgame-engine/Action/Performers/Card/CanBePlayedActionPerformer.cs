using boardgames_sharp.Entity;
using boardgames_sharp.Player;
using example_cardgame.Constants;

namespace example_cardgame.Action.Card;

public interface ICanBePlayedActionPerformer
{
    void PlayCard(EntityId entityId, PlayerId playerId, IReadOnlySet<EntityId> targets);
}
internal class CanBePlayedActionPerformer(ICardGameActionPerformer performer):ICanBePlayedActionPerformer
{
    public void PlayCard(EntityId entityId, PlayerId owner, IReadOnlySet<EntityId> targets)
    {
        var entity = performer.BasePerformer.Entity.get_entity(entityId);
        var foundAction = entity.ActionProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.ACTION.CARD_ONPLAY, out var action);
        if (!foundAction || action == null)
        {
            throw new Exception("did not have CARD_ONPLAY");
        }
        var foundCooldown = entity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.CAN_BE_PLAYED_COOLDOWN, out var cooldown);
        if (!foundCooldown || cooldown == null)
        {
            throw new Exception("did not have CAN_BE_PLAYED_COOLDOWN");
        }
        var foundCurrentCooldown = entity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.CAN_BE_PLAYED_CURRENT_COOLDOWN, out var currentCooldown);
        if (!foundCurrentCooldown || currentCooldown == null)
        {
            throw new Exception("did not have  CAN_BE_PLAYED_CURRENT_COOLDOWN");
        }

        if (cooldown > currentCooldown)
        {
            performer.BasePerformer.Entity.add_modifier_set_value(entityId,
                CONSTANTS.PROPERTY_IDS.INT.CAN_BE_PLAYED_CURRENT_COOLDOWN, cooldown);
        }

        performer.BasePerformer.Stack.add_action_to_stack(action:action, owner:owner, targets:targets, source:entityId);
    }
}