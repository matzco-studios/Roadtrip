using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class JSceneTest
{
    private GameObject _door;
    
    // A Test behaves as an ordinary method
    [Test]
    public void JSceneTestSimplePasses()
    {
    }

    [UnityTest]
    public IEnumerator JSceneTestWithEnumeratorPasses()
    {
        yield return null;
    }
}
