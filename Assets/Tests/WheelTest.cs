using System.Collections;
using System.Collections.Generic;
using Car.Parts;
using Items;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.TestTools;

public class WheelTest
{
    private Wheel wheel;
    private GameObject wheelGO;
    
    /// <summary>
    ///     Setup Wheel
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        wheelGO = new GameObject();

        wheel = new Wheel()
        {
            wheelObject = wheelGO,
            wheelCollider = wheelGO.gameObject.AddComponent<WheelCollider>(),
        };
        wheel.AddPressure(Wheel.MaxPsi);
    }


    [TearDown]
    public void Teardown()
    {
        Object.Destroy(wheelGO);
    }


    /// <summary>
    ///     Test when FlatTire method is called
    ///     pressure should be at 0 
    /// </summary>
    [Test]
    public void When_Wheel_FlatTire_Expect_pressureIsZero()
    {
        wheel.FlatTire();
        Assert.AreEqual(wheel.Pressure, 0);
    }

    /// <summary>
    ///     Test when AddPressure method is called
    ///     should increase the pressure by a certain
    ///     amount, in this case by 10
    /// </summary>
    [Test]
    public void When_Wheel_AddPressure_Expect_pressureGreater()
    {
        wheel.FlatTire();
        var oldPressure = wheel.Pressure;
        
        wheel.AddPressure(10);
        Assert.Greater(wheel.Pressure, oldPressure);
    }

    /// <summary>
    ///     Test when ReducePressure method is called
    ///     should reduce the pressure by a certain
    ///     amount, in this case by 10 
    /// </summary>
    [Test]
    public void When_Wheel_ReducePressure_Expect_pressureLessThanLast()
    {
        wheel.ReducePressure(10);
        Assert.Less(wheel.Pressure, Wheel.MaxPsi);
    }
}
