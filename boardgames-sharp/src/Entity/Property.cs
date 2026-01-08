namespace boardgames_sharp.Entity;

public interface IPropertyReadOnly<T>
{
    T CurrentValue();
}
public class Property<T>(T zeroValue):IPropertyReadOnly<T>
{
    private T _currentValue = default!;
    private bool _needsRecalculation = true;

    public T CurrentValue()
    {
        if (_needsRecalculation)
        {
            _needsRecalculation = false;
            _currentValue = Recalculate();
        }

        return _currentValue;
    }

    public void AddModifier(IPropertyModifier<T> modifier)
    {
        _needsRecalculation = true;
        _modifiers.Add(modifier);
    }
    private T Recalculate()
    {
        var value = zeroValue;
        foreach(var modifier in _modifiers)
        {
            value = modifier.apply_modifier(value);
        }

        return value;
    }
    
    private readonly List<IPropertyModifier<T>> _modifiers = new();
}
