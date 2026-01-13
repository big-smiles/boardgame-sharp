using example_cardgame.Constants;
using example_cardgame.PlayerCharacter;

namespace example_cardgame.Action.PlayerCharacter;

public interface IPlayerCharacterActionPerformer
{
    IPlayerCharacter create_player_character(int startingHealth);
}
internal class PlayerCharacterActionPerformer(ICardGameActionPerformer performer): IPlayerCharacterActionPerformer
{
    public IPlayerCharacter create_player_character(int startingHealth)
    {
        var entity = performer.BasePerformer.Entity.create_entity();
        performer.BasePerformer.Entity.add_property(entity.Id, CONSTANTS.PROPERTY_IDS.INT.ENTITY_TYPE,
            CONSTANTS.ENTITY_TYPES.PLAYER_CHARACTER);
        performer.BasePerformer.Entity.add_property(entity.Id, CONSTANTS.PROPERTY_IDS.INT.PLAYER_HEALTH, startingHealth);
        return new example_cardgame.PlayerCharacter.PlayerCharacter(entity);
    }
}