namespace boardgames_sharp.Actions.ActionPerformer.Entity;

public partial class EntityActionPerformer
{
    public boardgames_sharp.Entity.Entity CreateEntity()
    {
        if (_entityManager == null)
        {
            throw new NullReferenceException("Entity Manager not initialized in EntityActionPerformer");
        }
        var entity = _entityManager.create_empty_entity();
        return entity;
    }
}