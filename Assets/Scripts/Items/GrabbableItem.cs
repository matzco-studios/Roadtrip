using UnityEngine;

public abstract class GrabbableItem : MonoBehaviour
{
    public Quaternion Rotation;
    public string Name;

    public abstract void OnRightClick();

    public abstract void OnLeftClick();

    public abstract void OnCustomAction();

    public GrabbableItem(Quaternion rotation)
    {
        Rotation = rotation;
    }

    public GrabbableItem() {}
}
