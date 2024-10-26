using System;
using UnityEngine;

[Serializable]
public class Wheel
{
    public const int MaxPsi = 32;
    public GameObject wheelObject;
    public WheelCollider wheelCollider;
    public bool isFrontWheel;
    private int _pressure = 60;
        
    public int Pressure { get => _pressure; }
    public void FlatTire() => _pressure = 0;
    public void AddPressure(int amount) => _pressure = Math.Clamp(_pressure + amount, 0, MaxPsi);
    public void ReducePressure(int amount) => _pressure = Math.Clamp(_pressure - amount, 0, MaxPsi);
}