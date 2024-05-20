namespace AGK.Domain.ValueObjects;

public abstract record ValueObject
{
    public abstract IEnumerable<object> GetAtomicValues();

    public bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }
}
