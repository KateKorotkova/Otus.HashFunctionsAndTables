namespace Otus.HashFunctionsAndTables.Logic
{
    public class Entry<TK, TV>
    {
        public TK Key { get; set; }
        public TV Value { get; set; }
        public Entry<TK, TV> Next { get; set; }

        public Entry(TK key, TV value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"Key - {Key}, Value - {Value}, HasNext - {Next != null}";
        }
    }
}
