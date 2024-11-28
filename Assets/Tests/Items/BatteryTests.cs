using System.Collections;
using System.Collections.Generic;
using Car;
using Items;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BatteryTests
{
    private GameObject _battery;
    private Battery _controller;
    private GameObject _batteryCollider;
    private GameObject _car;
    private CarController _carController;

    [SetUp]
    public void SetUp()
    {
        _battery = new GameObject("Battery");
        _controller = _battery.AddComponent<Battery>();
        _battery.tag = "GrabbableItem";
        _batteryCollider = new GameObject("BatteryCollider");
        _batteryCollider.tag = "BatteryCollider";
        _car = new GameObject("Car");
        _carController = _car.AddComponent<CarController>();
    }

    [Test]
    public void When_ReducingBatteryHealthBy20_Except_CurrentHealthEqualsOldHealthMinus20()
    {
        var currentHealth = _controller.Health;
        _controller.ReduceHealth(20);
        Assert.AreEqual(currentHealth - 20, _controller.Health);
    }

    [Test]
    public void When_KillingBattery_Except_CurrentHealthEquals0()
    {
        _controller.SetDead();
        Assert.AreEqual(_controller.Health, 0);
    }

   /*[UnityTest]
    public void When_BatteryIsGrabbedFromCar_Except_CarControllerToBeNull()
    {
        _controller.OnTriggerEnter(_batteryCollider)
        _controller.SetDead();
        Assert.AreEqual(_controller.Health, 0);
    }*/
}
