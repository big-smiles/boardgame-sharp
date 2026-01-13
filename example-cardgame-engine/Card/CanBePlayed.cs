using boardgames_sharp.Entity;
using example_cardgame.Action;
using example_cardgame.Constants;

namespace example_cardgame.Card;

public interface ICanBePlayed
{ 
    int Cooldown { get; }
}

public interface ICanBePlayedData
{
    int Cooldown { get; }
    CardGameAction OnPlay { get; }
}
internal class CanBePlayedData : ICanBePlayedData
{
    public int Cooldown { get; set; }
    public CardGameAction OnPlay { get; set; }
}
public class CanBePlayed:ICanBePlayed
{
    public CanBePlayed(StateEntity stateEntity)
    {
        var foundCooldown = stateEntity.IntProperties.TryAndGet(CONSTANTS.PROPERTY_IDS.INT.CAN_BE_PLAYED_COOLDOWN, out var cooldown);
        
        if (!foundCooldown || cooldown == null)
        {
            throw new Exception("cooldown not found");
        }

        Cooldown = cooldown;
    }

    public CanBePlayed(IEntityReadOnly entityReadOnly)
    {
        var foundCooldown = entityReadOnly.try_and_get_value_of_type(CONSTANTS.PROPERTY_IDS.INT.CAN_BE_PLAYED_COOLDOWN, out var cooldown);
        
        if (!foundCooldown || cooldown == null)
        {
            throw new Exception("cooldown not found");
        }
        
        Cooldown = cooldown;
    }
    
    
    public int Cooldown { get; }
    
}