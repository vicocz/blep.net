namespace Blep.Contract
{
    public interface IFactory<TKey, TValue>
    {
        bool TryCreate(TKey key, out TValue value);
    }
}
