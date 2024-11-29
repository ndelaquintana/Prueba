namespace Carglass.TechnicalAssessment.Backend.Entities;

public class Key(params object[] values)
{
    public override bool Equals(object obj)
    {
        if (obj is Key key)
            return GetHashCode() == key.GetHashCode();
        return false;
    }
    public override int GetHashCode() =>
        CollectionHashCode(values);
    public override string ToString() =>
        $"Key({string.Join(',', Values)})";
    public object[] Values =>
        values;

    public static implicit operator Object[](Key key) =>
        key.Values;
    public static implicit operator Key(Object[] values) =>
        new Key(values);

    public static bool operator ==(Key key1, Key key2) =>
        key1.GetHashCode() == key2.GetHashCode();
    public static bool operator !=(Key key1, Key key2) =>
        key1.GetHashCode() != key2.GetHashCode();

    private static int CollectionHashCode(params object[] values)
    {
        var hash = new HashCode();
        foreach (var value in values)
            hash.Add(value);
        return hash.ToHashCode();
    }
}