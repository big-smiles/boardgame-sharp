namespace boardgames_sharp.Actions.ActionPerformer.Entity;

internal sealed partial class EntityActionPerformer
{
    public boardgames_sharp.Entity.Entity create_entity()
    {
        if (_entityManager == null)
        {
            throw new NullReferenceException("Entity Manager not initialized in EntityActionPerformer");
        }
        var entity = _entityManager.create_empty_entity();
        return entity;
    }
}