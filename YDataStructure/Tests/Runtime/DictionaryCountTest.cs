using NUnit.Framework;

namespace YellowJelloGames.YDataStructure.Tests.Runtime
{
    public class DictionaryCountTest
    {
        [Test]
        public void IncreaseCountTest()
        {
            var dictionary = new DictionaryCount<string>();

            dictionary.Increase("One");
            dictionary.Increase("Two");
            dictionary.Increase("One");

            Assert.AreEqual(2, dictionary["One"]);
        }
        
        [Test]
        public void DecreaseCountTest()
        {
            var dictionary = new DictionaryCount<string>();

            dictionary.Increase("One", 2);
            dictionary.Decrease("One");

            Assert.AreEqual(1, dictionary["One"]);
        }
    }
}