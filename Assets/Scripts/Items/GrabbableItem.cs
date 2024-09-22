using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabbableItem : MonoBehaviour
{
    [SerializeField] protected string Name;
    [SerializeField] protected Vector3 Rotation;

    public abstract void OnRightClick();
    
    public abstract void OnLeftClick();

    public void OnCustomAction() {}
}
