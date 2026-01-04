namespace boardgames_sharp.Entity;

public interface IPropertyModifier<T>
{
    T Apply(T currentValue);
}
