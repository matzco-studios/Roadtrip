using System;
using UnityEngine;

[Serializable]
public class Wheel
{
    public const float MaxPsi = 38;
    public GameObject wheelObject;
    public WheelCollider wheelCollider;
    public bool isFrontWheel;
    private float _pressure = 33;
        
    public float Pressure { get => _pressure; }
    public void FlatTire() => _pressure = 0;
    public void AddPressure(float amount) => _pressure = Math.Clamp(_pressure + amount, 0, MaxPsi);
    public void ReducePressure(float amount) => _pressure = Math.Clamp(_pressure - amount, 0, MaxPsi);
}