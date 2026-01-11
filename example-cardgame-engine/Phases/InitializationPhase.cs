using boardgames_sharp.Entity;
using boardgames_sharp.Phases;
using boardgames_sharp.Stack;
using example_cardgame.Action;
using example_cardgame.Card;
using example_cardgame.Constants;

namespace example_cardgame.Phases;

public class InitializationPhase(List<ICardData> cardsOnPlayerDeck):IPhase
{
    public bool Next(IActionStack actionStack)
    {
        new CardGameAction(_do, _undo);
        actionStack.AddPhaseAction(new CardGameAction(_do, _undo));
        return false;
    }

    public void _do(ICardGameActionPerformer performer, HashSet<EntityId> entityIds)
    {
        var playerDeck = performer.Container.create_container();
        performer.BasePerformer.Entity.save_entity(playerDeck.Id,CONSTANTS.KEY_ENTITY_IDS.PLAYER_DECK);
        foreach (var cardData in cardsOnPlayerDeck)
        {
            performer.Card.create_card_on_player_deck(cardData);
        }
        performer.BasePerformer.Phase.Pause();
        performer.BasePerformer.GameState.PublishNew();
    }

    public void _undo(ICardGameActionPerformer performer, HashSet<EntityId> entityIds)
    {
        
    }

}