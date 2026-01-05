namespace boardgames_sharp.Entity.Modifiers;

public class ModifierSetValue<T>(T value): IPropertyModifier<T>
{
    public T apply_modifier(T currentValue)
    {
        return value;
    }
}