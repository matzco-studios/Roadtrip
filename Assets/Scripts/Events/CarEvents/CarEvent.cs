using UnityEngine;

public abstract class CarEvent : MonoBehaviour
{
    protected CarController Car;

    private void Start()
    {
       Car = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
    }

    public abstract void Activate();
}
