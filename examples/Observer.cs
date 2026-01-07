using boardgames_sharp;
using boardgames_sharp.Entity;
using boardgames_sharp.GameState;
using boardgames_sharp.Interaction;
using examples.Phases;

namespace examples;


internal class StateObserver(Board board, Input input, Engine engine) : IObserver<IGameState>
{
    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw error;
    }

    private int count = 0;
    public void OnNext(IGameState state)
    {
        Console.WriteLine("Observer count=" + count++);
        foreach (var tuple in state.Entities.Entities)
        {
            var entity = tuple.Item2;
            int x = 0;
            int y = 0;
            int value = 0;
            foreach (var propertyTuple in entity.IntProperties.Properties)
            {
                var propertyId = propertyTuple.Item1;
                var propertyValue = propertyTuple.Item2;
                if (propertyId.Equals(Constants.XIntPropertyId))
                {
                    x = propertyValue;
                } else if (propertyId.Equals(Constants.YIntPropertyId))
                {
                    y = propertyValue;
                } else if (propertyId.Equals(Constants.ValueIntPropertyId))
                {
                    value = propertyValue;
                }
            }
            board.SetCell(x, y, value);
        }
        board.Draw();
        if (state.WinnerPlayerId != null)
        {
            if (state.WinnerPlayerId.Equals(Constants.Player1))
            {
                Console.WriteLine("Player X won!");
            }
            else
            {
                Console.WriteLine("Player O won!");
            }

            return;

        }
        //TODO this obviously is bullcrap, we are not directing the interaction but only assuming shit
        if (state.AvailableInteractions.Count > 0)
        {
            var availableInteraction = state.AvailableInteractions[0];
            Console.WriteLine("GameStateID=" + state.Id + " interactionId=" + availableInteraction.InteractionId);
            var selectedTuple = input.GetInput();
            var sectedX = selectedTuple.Item1;
            var sectedY = selectedTuple.Item2;
            var entityId = findCellWithXY(state.Entities, sectedX, sectedY);
            var interaction = new Interaction(availableInteraction.InteractionId, new HashSet<EntityId>() { entityId });
            engine.Interact(interaction);
        }
    }

    private EntityId findCellWithXY(StateEntities entities, int x, int y)
    {
        foreach (var tuple in entities.Entities)
        {
            var entityId = tuple.Item1;
            var entity = tuple.Item2;
            if (x != entity.IntProperties.Get(Constants.XIntPropertyId))
            {
                continue;
            }

            if (y != entity.IntProperties.Get(Constants.YIntPropertyId))
            {
                continue;
            }
            return entityId;
        }
        throw new Exception("Entity not found");
    }
}


