namespace Otus.HashFunctionsAndTables.Logic
{
    public class Entry
    {
        public int Key { get; set; }
        public string Value { get; set; }
        public Entry Next { get; set; }

        public Entry(int key, string value)
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
