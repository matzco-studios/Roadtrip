using System.Collections.Generic;
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
    public GameObject _player;
    private GameObject _scorchletPrefab;


    [SetUp]
    public void SetUp()
    {
        _truck = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PlayerCar.prefab");
        _player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/TemporaryPlayer.prefab");
        _scorchletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Scorchlet_monster.prefab");
        _truck = Object.Instantiate(_truck);
        _player = Object.Instantiate(_player);
    }
    
    [Test]
    public void ScorchletSpawnTest()
    {
        // _scorchletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Scorchlet_monster.prefab");
        _scorchlet = Object.Instantiate(_scorchletPrefab);
        Assert.That(_scorchlet, Is.Not.Null);
    }

    // [Test]
    // public void ScorchletTriggerTest()
    // {
        // _scorchletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Scorchlet_monster.prefab");
    //     _scorchlet = Object.Instantiate(_scorchletPrefab);
    //     _truck.transform.position = new Vector3(0, 0, 0);
    //     _player.transform.position = new Vector3(11, 0, 0);
    //     Assert.That(_scorchlet, Is.Not.Null);
    // }


    [TearDown]
    public void Teardown()
    {
        Object.Destroy(_scorchlet);
        Object.Destroy(_truck);
        Object.Destroy(_player);
    }

}