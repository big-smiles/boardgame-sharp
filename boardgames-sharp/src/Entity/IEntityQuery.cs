namespace boardgames_sharp.Entity.Modifiers;

public interface IEntityQuery
{
    public bool select_entity(IEntityReadOnly entity);
}