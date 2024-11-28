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
    ///     Setup FlashLight
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        wheelGO = new GameObject();

        wheel = new Wheel()
        {
            wheelObject = wheelGO,
            wheelCollider = wheelGO.gameObject.AddComponent<WheelCollider>()
        };
    }

    [Test]
    public void When_Wheel_FlatTire_Expect_pressureIsZero ()
    {
        wheel.FlatTire();
        Assert.AreEqual(wheel.Pressure, 0);
    }
}
