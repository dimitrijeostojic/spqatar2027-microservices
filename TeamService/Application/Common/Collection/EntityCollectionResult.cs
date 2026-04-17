namespace Application.Common.Collection;

public abstract class EntityCollectionResult<T>
{
    public IReadOnlyCollection<T> Items { get; private set; }
    public int TotalCount { get; private set; }
    protected EntityCollectionResult(IEnumerable<T> items)
    {
        Items = [.. items];
        TotalCount = Items.Count;
    }
}
