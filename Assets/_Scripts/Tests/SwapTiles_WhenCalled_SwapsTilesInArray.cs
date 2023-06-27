using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SwapTiles_WhenCalled_SwapsTilesInArray
    {
        // A Test behaves as an ordinary method
        [Test]
        public void SwapTiles_WhenCalled_SwapsTilesInArraySimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator SwapTiles_WhenCalled_SwapsTilesInArrayWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
