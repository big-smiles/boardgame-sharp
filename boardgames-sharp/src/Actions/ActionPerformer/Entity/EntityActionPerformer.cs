using boardgames_sharp.Entity;

namespace boardgames_sharp.Actions.ActionPerformer.Entity;

public interface IEntityActionPerformer
{
    boardgames_sharp.Entity.Entity CreateEntity();
}
public partial class EntityActionPerformer: IEntityActionPerformer, IInitializeWithEngineRoot
{
    public void Initialize(EngineRoot engineRoot)
    {
        _entityManager = engineRoot.EntityManager;
    }
    
    private IEntityManager? _entityManager;
}