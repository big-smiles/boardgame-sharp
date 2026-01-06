using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;

namespace boardgames_sharp.Tests;

internal delegate bool DEntityQuery(IEntityReadOnly entity);
internal class TestEntityQuery(DEntityQuery entityQuery) : IEntityQuery
{
    public bool select_entity(IEntityReadOnly entity)
    {
        return entityQuery(entity);
    }
}