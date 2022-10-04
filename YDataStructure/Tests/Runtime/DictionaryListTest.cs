using NUnit.Framework;

namespace YellowJelloGames.YDataStructure.Tests.Runtime
{
    public class DictionaryListTest
    {
        private const string KEY1 = "Apple";
        private const string KEY2 = "Banana";

        private const string VALUE1 = "1.49";
        private const string VALUE2 = "1.48";

        [Test]
        public void AddEntryToDictionaryTest()
        {
            var dict = new DictionaryList<string, string>();

            dict.Add(KEY1, VALUE1);
            Assert.AreEqual(1, dict[KEY1].Count);

            dict.Add(KEY1, VALUE2);
            Assert.AreEqual(2, dict[KEY1].Count);

            dict.Add(KEY2, VALUE1);
            Assert.AreEqual(1, dict[KEY2].Count);
        }


        [Test]
        public void RemoveEntryFromDictionaryTest()
        {
            var dict = new DictionaryList<string, string>
            {
                { KEY1, VALUE1 },
                { KEY1, VALUE2 }
            };

            dict.Remove(KEY1, VALUE1);
            Assert.AreEqual(1, dict[KEY1].Count);
            
            dict.Remove(KEY1, VALUE2);
            Assert.IsFalse(dict.Contains(KEY1));
        }
    }
}