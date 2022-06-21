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
            var hashTable = new CustomHashTableWithChain(_capacity);

            var key = 1;
            var value = _random.Next().ToString();
            hashTable.Put(key, value);

            Assert.That(hashTable.Buckets[1].Key, Is.EqualTo(key));
            Assert.That(hashTable.Buckets[1].Value, Is.EqualTo(value));
        }

        [Test]
        public void Can_Put_With_One_Collision()
        {
            var hashTable = new CustomHashTableWithChain(_capacity);

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
            var hashTable = new CustomHashTableWithChain(_capacity);

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
        public void Can_Get_Without_Collision()
        {
            var hashTable = new CustomHashTableWithChain(_capacity);
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
            var hashTable = new CustomHashTableWithChain(_capacity);
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
            var hashTable = new CustomHashTableWithChain(_capacity);
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
    }
}