using System;

namespace Otus.HashFunctionsAndTables.Logic
{
    public class CustomHashTableWithChain<TKey, TValue>
    {
        private int _capacity;
        private int _size;
        private readonly float _loadFactor = 0.5f;
        private int Threshold => (int) (_capacity * _loadFactor);

        public Entry<TKey, TValue>[] Buckets { get; set; }

        public CustomHashTableWithChain(int capacity)
        {
            _capacity = capacity;
            Buckets = new Entry<TKey, TValue>[_capacity];
        }


        public void Put(TKey key, TValue value)
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

                existedEntry = new Entry<TKey, TValue>(key, value);
                Buckets[indexInBuckets] = existedEntry;
            }
            else
            {
                do
                {
                    if (existedEntry.Next == null)
                    {
                        existedEntry.Next = new Entry<TKey, TValue>(key, value);
                        return;
                    }

                    if (existedEntry.Next.Key.Equals(key))
                    {
                        existedEntry.Next.Value = value;
                    }

                    existedEntry = existedEntry.Next;
                } 
                while (existedEntry != null);
            }
        }

        public Entry<TKey, TValue> Get(TKey key)
        {
            var indexInBuckets = GetHash(key);
            
            var existedEntry = Buckets[indexInBuckets];
            if (existedEntry == null)
                return null;

            if (existedEntry.Key.Equals(key))
                return existedEntry;

            do
            {
                if (existedEntry.Next == null)
                {
                    return null;
                }

                if (existedEntry.Next.Key.Equals(key))
                {
                    return existedEntry.Next;
                }

                existedEntry = existedEntry.Next;
            }
            while (existedEntry != null);

            return null;
        }

        public bool Remove(TKey key)
        {
            var indexInBuckets = GetHash(key);

            var existedEntry = Buckets[indexInBuckets];
            if (existedEntry == null)
                return false;

            Entry<TKey, TValue> previousEntry = null;
            while (existedEntry != null)
            {
                if (existedEntry.Key.Equals(key))
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

        private void Rehash()
        {
            _capacity = Buckets.Length * 2;
            
            var oldBuckets = Buckets;
            Buckets = new Entry<TKey, TValue>[_capacity];

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

        private int GetHash(TKey key)
        {
            return Math.Abs(key.GetHashCode() % Buckets.Length);
        }

        #endregion
    }
}
