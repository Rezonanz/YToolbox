using NUnit.Framework;

namespace YellowJelloGames.YDataStructure.Tests.Runtime
{
    public class DictionaryDelegateTest
    {
        private delegate void TestDelegate();

        [Test]
        public void InvokeTest()
        {
            bool isComplete = false;

            var dict = new DictionaryDelegate<int, TestDelegate>();
            dict.AddListener(0, () => isComplete = true);
            dict[0].Invoke();

            Assert.IsTrue(isComplete);
        }


        [Test]
        public void RemoveListenerTest()
        {
            var dict = new DictionaryDelegate<int, TestDelegate>();

            void LocalCallback()
            {
                // empty test delegate
            }

            dict.AddListener(0, LocalCallback);
            dict.RemoveListener(0, LocalCallback);
            
            Assert.IsNull(dict[0]);
        }
    }
}