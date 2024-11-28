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

    [SetUp]
    public void SetUp()
    {
        _battery = new GameObject("Battery");
        _controller = _battery.AddComponent<Battery>();
        // To ensure that the health is at 100
        _controller.AddHealth(100);
        _battery.tag = "GrabbableItem";
    }

    [Test]
    public void When_Adding50ToBatteryHealth_Except_CurrentHealthEqualsOldHealthPlus50()
    {
        // To set the health to zero.
        _controller.SetDead();

        var oldHealth = _controller.Health;
        _controller.AddHealth(50);
        Assert.AreEqual(_controller.Health, oldHealth + 50);
    }

    [Test]
    public void When_ReducingBatteryHealthBy20_Except_CurrentHealthEqualsOldHealthMinus20()
    {
        _controller.ReduceHealth(20);
        Assert.AreEqual(_controller.Health, 80);
    }

    [Test]
    public void When_KillingBattery_Except_CurrentHealthEquals0()
    {
        _controller.SetDead();
        Assert.AreEqual(_controller.Health, 0);
    }
}
