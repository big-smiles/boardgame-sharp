namespace boardgames_sharp.Entity;

public class Property<T>(T zeroValue)
{
    private T _currentValue = default!;
    private bool _needsRecalculation = true;

    public T CurrentValue()
    {
        if (!_needsRecalculation)
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
            value = modifier.Apply(value);
        }

        return value;
    }
    
    private readonly List<IPropertyModifier<T>> _modifiers = new();
}
