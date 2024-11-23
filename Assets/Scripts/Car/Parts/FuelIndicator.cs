using UnityEngine;

namespace Car.Parts
{
    public class FuelIndicator : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private float aFull = 0;
        [SerializeField] private float aEmpty = 90;
        private CarController _car;

        void Start()
        {
            _car = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
            // _car = transform.parent.GetComponent<CarController>();
        }

        // Update is called once per frame
        void Update()
        {
            float fuel = _car.currentFuel / 100f;
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(aEmpty, aFull, fuel));
        }
    }
}