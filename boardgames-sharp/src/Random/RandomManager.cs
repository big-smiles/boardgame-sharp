namespace boardgames_sharp.Random;

public interface IRandomManager
{
    bool try_pick_random_elements<T>(IList<T> source, int count, out List<T> result);
    T pick_weighted_random<T>(IList<(float weight, T value)> items);
}
public class RandomManager:IRandomManager
{
    public bool try_pick_random_elements<T>(
        IList<T> source,
        int count,
        out List<T> result)
    {
        // If caller passed an existing list, reuse it
       
        result = new List<T>(count);

        if (source == null || _rng == null)
            return false;

        if (count < 0 || count > source.Count)
            return false;

        if (count == 0)
            return true;

        // Copy to avoid mutating the original list
        var temp = new List<T>(source);

        // Partial Fisher–Yates shuffle
        for (int i = 0; i < count; i++)
        {
            int j = _rng.Next(i, temp.Count);
            (temp[i], temp[j]) = (temp[j], temp[i]);
        }

        // Fill the reused list
        for (int i = 0; i < count; i++)
            result.Add(temp[i]);

        return true;
    }
    public T pick_weighted_random<T>(IList<(float weight, T value)> items)
    {
        if (items == null || items.Count == 0)
            throw new ArgumentException("Items list is empty.");

        float totalWeight = 0f;

        foreach (var (weight, _) in items)
        {
            if (weight < 0)
                throw new ArgumentException("Weights cannot be negative.");
            totalWeight += weight;
        }

        if (totalWeight <= 0f)
            throw new InvalidOperationException("Total weight must be > 0.");

        float r = (float)(_rng.NextDouble() * totalWeight);
        float cumulative = 0f;

        foreach (var (weight, value) in items)
        {
            cumulative += weight;
            if (r <= cumulative)
                return value;
        }

        // Fallback (por floating point edge cases)
        return items[^1].value;
    }

    private readonly System.Random _rng = new System.Random();
}