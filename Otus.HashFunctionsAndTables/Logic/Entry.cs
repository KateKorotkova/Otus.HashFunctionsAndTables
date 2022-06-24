namespace Otus.HashFunctionsAndTables.Logic
{
    public class Entry<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Entry<TKey, TValue> Next { get; set; }

        public Entry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"Key - {Key}, Value - {Value}, HasNext - {Next != null}";
        }

        public bool IsEqual(TKey key, TValue value)
        {
            return Key.Equals(key) && Value.Equals(value);
        }
    }
}
