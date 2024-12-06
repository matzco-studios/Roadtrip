using System.Collections;
using System.Threading;
using Enemies.Scorchlet;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

public class ScorchletTest
{
    private GameObject _scorchlet;
    private GameObject _truck;
    private GameObject _player;
    private GameObject _scorchletPrefab;


    [SetUp]
    public void SetUp()
    {
        // _truck = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PlayerCar.prefab");
        // _player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/TemporaryPlayer.prefab");
        _scorchletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Scorchlet_monster.prefab");
        // _truck = Object.Instantiate(_truck);
        // _truck.AddComponent<ScorchletSpawn>();
        // _player = Object.Instantiate(_player);
    }

    [Test]
    public void ScorchletSpawnTest()
    {
        _scorchlet = Object.Instantiate(_scorchletPrefab);
        Assert.That(_scorchlet, Is.Not.Null);
    }

    // [UnityTest]
    // public IEnumerator ScorchletMovement()
    // {
    //     _scorchlet = new GameObject();
    //     _scorchlet.AddComponent<ScorchletController>();
    //     _scorchlet.transform.position = new Vector3(0, 0, 0);
    //     _truck = new GameObject();
    //     _truck.transform.position = new Vector3(10, 0, 10);
    //     yield return new WaitForSeconds(10f);
    //     Assert.AreNotEqual(_scorchlet.transform.position, new Vector3(0, 0, 0));
    // }

    [Test]
    public void ScorchletFlee()
    {
        _scorchlet = new GameObject();
        _scorchlet.AddComponent<ScorchletController>();
        _scorchlet.transform.position = new Vector3(0, 0, 0);
        _scorchlet.GetComponent<ScorchletController>().IsFlashed();
        Assert.IsTrue(_scorchlet.GetComponent<ScorchletController>().IsWatched);
    }


    [TearDown]
    public void Teardown()
    {
        Object.Destroy(_scorchlet);
        Object.Destroy(_truck);
        Object.Destroy(_player);
    }

}
