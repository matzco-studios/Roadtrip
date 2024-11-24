using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Car.Parts
{
    public class Warnings : MonoBehaviour
    {
        private CarController _car;
        
        private GameObject 
            _batteryIndicator, 
            _pressureIndicator,
            _fuelIndicator,
            _fuelBar;
        
        [SerializeField] private float aFull, aEmpty = 90;
        
        private void Awake()
        {
            _car = transform.parent.GetComponent<CarController>();

            _fuelBar           = transform.GetChild(0).gameObject;
            _fuelIndicator     = transform.GetChild(1).gameObject;
            _batteryIndicator  = transform.GetChild(2).gameObject;
            _pressureIndicator = transform.GetChild(3).gameObject;
        }
        
        IEnumerator BlinkFuel()
        {
            for (;;)
            {
                yield return new WaitForSeconds(.5f);
                _fuelIndicator.SetActive(false);
                
                if ((_car.currentFuel < 30 && _car.currentFuel != 0) && _car.IsPlayerInside)
                {
                    yield return new WaitForSeconds(.5f);
                    _fuelIndicator.SetActive(true);
                }
            }
        }

        private void Start()
        {
            StartCoroutine(BlinkFuel());
        }

        private void Update()
        {
            _fuelBar.transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(aEmpty, aFull, _car.currentFuel / 100f));

            _batteryIndicator.SetActive((!_car.IsBatteryInside() || _car.Battery.IsDead()) && _car.IsPlayerInside);
            _pressureIndicator.SetActive(_car.wheels.Count(w => w.isFlat) >= 1 && _car.IsPlayerInside);
        }
    }
}
