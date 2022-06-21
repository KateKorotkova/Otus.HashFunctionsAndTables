using System;

namespace Otus.HashFunctionsAndTables.Logic
{
    public class CustomHashTableWithChain
    {
        private int _capacity;
        private int _size;
        private readonly float _loadFactor = 0.5f;
        private int Threshold => (int) (_capacity * _loadFactor);

        public Entry[] Buckets { get; set; }

        public CustomHashTableWithChain(int capacity)
        {
            _capacity = capacity;
            Buckets = new Entry[_capacity];
        }


        public void Put(int key, string value)
        {
            var indexInBuckets = GetHash(key);

            var existedEntry = Buckets[indexInBuckets];
            if (existedEntry == null)
            {
                if (++_size > Threshold)
                {
                    Rehash();
                    indexInBuckets = GetHash(key);
                }

                existedEntry = new Entry(key, value);
                Buckets[indexInBuckets] = existedEntry;
            }
            else
            {
                do
                {
                    if (existedEntry.Next == null)
                    {
                        existedEntry.Next = new Entry(key, value);
                        return;
                    }

                    if (existedEntry.Next.Key == key)
                    {
                        existedEntry.Next.Value = value;
                    }

                    existedEntry = existedEntry.Next;
                } 
                while (existedEntry != null);
            }
        }

        public Entry Get(int key)
        {
            var indexInBuckets = GetHash(key);
            
            var existedEntry = Buckets[indexInBuckets];
            if (existedEntry == null)
                return null;

            if (existedEntry.Key == key)
                return existedEntry;

            do
            {
                if (existedEntry.Next == null)
                {
                    return null;
                }

                if (existedEntry.Next.Key == key)
                {
                    return existedEntry.Next;
                }

                existedEntry = existedEntry.Next;
            }
            while (existedEntry != null);

            return null;
        }

        public bool Remove(int key)
        {
            var indexInBuckets = GetHash(key);

            var existedEntry = Buckets[indexInBuckets];
            if (existedEntry == null)
                return false;

            Entry previousEntry = null;
            while (existedEntry != null)
            {
                if (existedEntry.Key == key)
                {
                    if (previousEntry == null)
                    {
                        Buckets[indexInBuckets] = existedEntry.Next;
                    }
                    else
                    {
                        previousEntry.Next = existedEntry.Next;
                    }

                    _size--;
                    return true;
                }

                previousEntry = existedEntry;
                existedEntry = existedEntry.Next;
            }

            return false;
        }

        
        #region Support Methods

        private int GetHash(int key)
        {
            return Math.Abs(key.GetHashCode() % Buckets.Length);
        }

        private void Rehash()
        {
            _capacity = Buckets.Length * 2;
            
            var oldBuckets = Buckets;
            Buckets = new Entry[_capacity];

            for (var i = 0; i < oldBuckets.Length; i++)
            {
                var currentEntry = oldBuckets[i];
                if (currentEntry == null)
                    continue;

                var indexInNewBuckets = GetHash(currentEntry.Key);
                
                var existedEntry = Buckets[indexInNewBuckets];
                if (existedEntry == null)
                {
                    Buckets[indexInNewBuckets] = currentEntry;
                }
                else
                {
                    var previousNextEntry = existedEntry.Next;
                    existedEntry.Next = currentEntry;
                    currentEntry.Next = previousNextEntry;
                }
            }
        }

        #endregion
    }
}
