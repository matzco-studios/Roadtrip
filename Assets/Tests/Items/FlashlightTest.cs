using System.Collections;
using System.Collections.Generic;
using Items;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.TestTools;

public class FlashlightTest
{
    private FlashLight flashLight;
    private GameObject flashLightGO;
    private GameObject childrenGO; 

    /// <summary>
    ///     Setup FlashLight
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        flashLightGO = new GameObject("FlashLight");
        childrenGO = new GameObject("FlashLightChild");
        
        childrenGO.AddComponent<Light>();
        childrenGO.transform.SetParent(flashLightGO.transform);
        
        flashLight = flashLightGO.AddComponent<FlashLight>();
        
        flashLight.gameObject.AddComponent<Rigidbody>();
        flashLight.gameObject.AddComponent<Animator>();
        flashLight.gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    ///     Test if the light is turned on when F key is pressed 
    /// </summary>
    // [Test]
    // public void When_FlashLightKeyPressed_Expect_lightIsTurnedOn_()
    // {
    //     flashLight.LeftMouse();
    //     Assert.True(flashLight.IsTurnedOn);
    //     Assert.True(flashLight.GetComponent<Light>().enabled);
    // }

    /// <summary>
    ///     Test to check if flashlight battery is reduced
    ///     when light is turned on
    /// </summary>
    // [Test]
    // public void FlashLight_Reduced_Battery_When_Enabled()
    // {
    //     var oldBattery = flashLight.Battery;
    //     flashLight.LeftMouse();
    //     Assert.Greater(oldBattery, flashLight.Battery);
    // }
}
