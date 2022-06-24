using System;
using NUnit.Framework;
using Otus.HashFunctionsAndTables.Logic;

namespace Tests
{
    public class CustomHashTableWithChainTests
    {
        private int _capacity = 10;
        private Random _random;

        [OneTimeSetUp]
        public void SetUp()
        {
            _random = new Random();
        }

        [Test]
        public void Can_Put_Without_Collision()
        {
            var hashTable = new CustomHashTableWithChain<int, string>(_capacity);

            var key = 1;
            var value = _random.Next().ToString();
            hashTable.Put(key, value);

            Assert.That(hashTable.Buckets[1].Key, Is.EqualTo(key));
            Assert.That(hashTable.Buckets[1].Value, Is.EqualTo(value));
        }

        [Test]
        public void Can_Put_With_One_Collision()
        {
            var hashTable = new CustomHashTableWithChain<int, string>(_capacity);

            var firstKey = 1;
            var firstValue = _random.Next().ToString();
            var secondKey = 11;
            var secondValue = _random.Next().ToString();
            hashTable.Put(firstKey, firstValue);
            hashTable.Put(secondKey, secondValue);

            var firstEntry = hashTable.Buckets[1];
            Assert.That(firstEntry.Key, Is.EqualTo(firstKey));
            Assert.That(firstEntry.Value, Is.EqualTo(firstValue));
            Assert.That(firstEntry.Next.Key, Is.EqualTo(secondKey));
            Assert.That(firstEntry.Next.Value, Is.EqualTo(secondValue));
        }

        [Test]
        public void Can_Put_With_Two_Collisions()
        {
            var hashTable = new CustomHashTableWithChain<int, string>(_capacity);

            var firstKey = 1;
            var firstValue = _random.Next().ToString();
            var secondKey = 11;
            var secondValue = _random.Next().ToString();
            var thirdKey = 21;
            var thirdValue = _random.Next().ToString();
            hashTable.Put(firstKey, firstValue);
            hashTable.Put(secondKey, secondValue);
            hashTable.Put(thirdKey, thirdValue);

            var firstEntry = hashTable.Buckets[1];
            Assert.That(firstEntry.Key, Is.EqualTo(firstKey));
            Assert.That(firstEntry.Value, Is.EqualTo(firstValue));
            Assert.That(firstEntry.Next.Key, Is.EqualTo(secondKey));
            Assert.That(firstEntry.Next.Value, Is.EqualTo(secondValue));
            Assert.That(firstEntry.Next.Next.Key, Is.EqualTo(thirdKey));
            Assert.That(firstEntry.Next.Next.Value, Is.EqualTo(thirdValue));
        }

        [Test]
        public void Can_Put_With_Rehash_Without_Collisions()
        {
            var hashTable = new CustomHashTableWithChain<int, string>(1);

            var firstKey = 1;
            var firstValue = _random.Next().ToString();
            var secondKey = 2;
            var secondValue = _random.Next().ToString();
            var thirdKey = 3;
            var thirdValue = _random.Next().ToString();

            hashTable.Put(firstKey, firstValue);
            hashTable.Put(secondKey, secondValue);
            hashTable.Put(thirdKey, thirdValue);

            Assert.That(hashTable.Buckets[1].Key, Is.EqualTo(firstKey));
            Assert.That(hashTable.Buckets[1].Value, Is.EqualTo(firstValue));
            Assert.That(hashTable.Buckets[2].Key, Is.EqualTo(secondKey));
            Assert.That(hashTable.Buckets[2].Value, Is.EqualTo(secondValue));
            Assert.That(hashTable.Buckets[3].Key, Is.EqualTo(thirdKey));
            Assert.That(hashTable.Buckets[3].Value, Is.EqualTo(thirdValue));
        }

        [Test]
        public void Can_Get_Without_Collision()
        {
            var hashTable = new CustomHashTableWithChain<int, string>(_capacity);
            var key = 1;
            var value = _random.Next().ToString();
            hashTable.Put(key, value);

            var result = hashTable.Get(key);

            Assert.IsNotNull(result);
            Assert.That(result.Key, Is.EqualTo(key));
            Assert.That(result.Value, Is.EqualTo(value));
        }

        [Test]
        public void Can_Get_With_One_Collision()
        {
            var hashTable = new CustomHashTableWithChain<int, string>(_capacity);
            var firstKey = 1;
            var firstValue = _random.Next().ToString();
            var secondKey = 11;
            var secondValue = _random.Next().ToString();
            hashTable.Put(firstKey, firstValue);
            hashTable.Put(secondKey, secondValue);

            var result = hashTable.Get(secondKey);

            Assert.IsNotNull(result);
            Assert.That(result.Key, Is.EqualTo(secondKey));
            Assert.That(result.Value, Is.EqualTo(secondValue));
        }

        [Test]
        public void Can_Get_With_Two_Collisions()
        {
            var hashTable = new CustomHashTableWithChain<int, string>(_capacity);
            var firstKey = 1;
            var firstValue = _random.Next().ToString();
            var secondKey = 11;
            var secondValue = _random.Next().ToString();
            var thirdKey = 21;
            var thirdValue = _random.Next().ToString();
            hashTable.Put(firstKey, firstValue);
            hashTable.Put(secondKey, secondValue);
            hashTable.Put(thirdKey, thirdValue);

            var result = hashTable.Get(thirdKey);

            Assert.IsNotNull(result);
            Assert.That(result.Key, Is.EqualTo(thirdKey));
            Assert.That(result.Value, Is.EqualTo(thirdValue));
        }

        [Test]
        public void Can_Remove_Without_Collision()
        {
            var hashTable = new CustomHashTableWithChain<int, string>(_capacity);
            var key = 1;
            hashTable.Put(key, _random.Next().ToString());

            var result = hashTable.Remove(key);

            Assert.IsTrue(result);
            Assert.IsNull(hashTable.Buckets[1]);
        }

        [Test]
        public void Can_Remove_With_One_Collision()
        {
            var hashTable = new CustomHashTableWithChain<int, string>(_capacity);
            var firstKey = 1;
            var secondKey = 11;
            hashTable.Put(firstKey, _random.Next().ToString());
            hashTable.Put(secondKey, _random.Next().ToString());

            var result = hashTable.Remove(secondKey);

            Assert.IsTrue(result);
            Assert.That(hashTable.Buckets[1].Key, Is.EqualTo(firstKey));
            Assert.IsNull(hashTable.Buckets[1].Next);
        }

        [Test]
        public void Can_Remove_With_Two_Collisions()
        {
            var hashTable = new CustomHashTableWithChain<int, string>(_capacity);
            var firstKey = 1;
            var secondKey = 11;
            var thirdKey = 21;
            hashTable.Put(firstKey, _random.Next().ToString());
            hashTable.Put(secondKey, _random.Next().ToString());
            hashTable.Put(thirdKey, _random.Next().ToString());

            var result = hashTable.Remove(thirdKey);

            Assert.IsTrue(result);
            Assert.That(hashTable.Buckets[1].Key, Is.EqualTo(firstKey));
            Assert.That(hashTable.Buckets[1].Next.Key, Is.EqualTo(secondKey));
            Assert.IsNull(hashTable.Buckets[1].Next.Next);
        }

        [Test]
        public void Can_Remove_With_Two_Collisions_In_A_Middle()
        {
            var hashTable = new CustomHashTableWithChain<int, string>(_capacity);
            var firstKey = 1;
            var secondKey = 11;
            var thirdKey = 21;
            hashTable.Put(firstKey, _random.Next().ToString());
            hashTable.Put(secondKey, _random.Next().ToString());
            hashTable.Put(thirdKey, _random.Next().ToString());

            var result = hashTable.Remove(secondKey);

            Assert.IsTrue(result);
            Assert.That(hashTable.Buckets[1].Key, Is.EqualTo(firstKey));
            Assert.That(hashTable.Buckets[1].Next.Key, Is.EqualTo(thirdKey));
            Assert.IsNull(hashTable.Buckets[1].Next.Next);
        }
    }
}