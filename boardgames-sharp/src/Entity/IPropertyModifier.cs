namespace boardgames_sharp.Entity;

public interface IPropertyModifier<T>
{
    public T apply_modifier(T currentValue);
}
